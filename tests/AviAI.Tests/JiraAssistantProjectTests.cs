using System.Xml.Linq;

namespace AviAI.Tests;

/// <summary>
/// Verifies the repository assets required for Jira assistant support.
/// </summary>
public sealed class JiraAssistantProjectTests
{
    /// <summary>
    /// Confirms that the instructions define the Jira assistant trigger and the PowerShell-only scripting rule.
    /// </summary>
    [Fact]
    public void CopilotInstructions_DefineJiraAssistantTrigger_AndPowerShellScriptRule()
    {
        var instructionsPath = GetRepoFile(".github", "copilot-instructions.md");
        var instructions = File.ReadAllText(instructionsPath);

        Assert.Contains("I need Jira assistance", instructions);
        Assert.Contains("jira_assistant", instructions);
        Assert.Contains("Use PowerShell for repository scripts.", instructions);
    }

    /// <summary>
    /// Confirms that the server project explicitly includes the Jira ticket reader script.
    /// </summary>
    [Fact]
    public void ProjectFile_IncludesJiraTicketScriptAsContent()
    {
        var projectFilePath = GetRepoFile("src", "AviAI.Server", "AviAI.Server.csproj");
        var project = XDocument.Load(projectFilePath);
        var contentIncludes = project
            .Descendants("Content")
            .Select(element => (string?)element.Attribute("Include"))
            .ToList();

        Assert.Contains(@"scripts\jira\Get-JiraTicket.ps1", contentIncludes);
    }

    /// <summary>
    /// Confirms that the Jira ticket reader script uses the expected inputs and authentication environment variable.
    /// </summary>
    [Fact]
    public void JiraTicketScript_UsesExpectedInputs_AndJiraApiKeyEnvironmentVariable()
    {
        var scriptPath = GetRepoFile("src", "AviAI.Server", "scripts", "jira", "Get-JiraTicket.ps1");
        var scriptContents = File.ReadAllText(scriptPath);

        Assert.Contains("JIRA_APIKEY", scriptContents);
        Assert.Contains("[string]$BaseUrl", scriptContents);
        Assert.Contains("[string]$IssueKey", scriptContents);
        Assert.Contains("Invoke-RestMethod", scriptContents);
        Assert.Contains("/rest/api/latest/issue/", scriptContents);
    }

    private static string GetRepoFile(params string[] pathSegments)
    {
        var currentDirectory = new DirectoryInfo(AppContext.BaseDirectory);

        while (currentDirectory is not null && !File.Exists(Path.Combine(currentDirectory.FullName, "AviAI.slnx")))
        {
            currentDirectory = currentDirectory.Parent;
        }

        Assert.NotNull(currentDirectory);

        var combinedPathSegments = new string[pathSegments.Length + 1];
        combinedPathSegments[0] = currentDirectory!.FullName;
        Array.Copy(pathSegments, 0, combinedPathSegments, 1, pathSegments.Length);

        return Path.Combine(combinedPathSegments);
    }
}
