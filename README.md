# AviAI

AviAI is a .NET 10 solution with a server project and an xUnit test project.

## Jira Assistance

If you say `I need Jira assistance`, the instructions switch into `jira_assistant` mode for Jira-related help in this repository.

## Scripts

Repository automation scripts are written in PowerShell.

### Read a Jira Ticket

The Jira ticket reader script lives at [`src/AviAI.Server/scripts/jira/Get-JiraTicket.ps1`](src/AviAI.Server/scripts/jira/Get-JiraTicket.ps1).
The Jira config file lives at [`src/AviAI.Server/scripts/jira/config.jira.json`](src/AviAI.Server/scripts/jira/config.jira.json) and currently stores the default Jira host.

1. Set the Jira API key in your environment.

```powershell
$env:JIRA_APIKEY = "your-api-key"
```

2. Run the script with your Jira base URL and issue key.

```powershell
.\src\AviAI.Server\scripts\jira\Get-JiraTicket.ps1 -BaseUrl "https://your-company.atlassian.net" -IssueKey "PROJ-123"
```

3. Add `-Raw` if you want the full Jira response object instead of the summarized ticket fields.

```powershell
.\src\AviAI.Server\scripts\jira\Get-JiraTicket.ps1 -BaseUrl "https://your-company.atlassian.net" -IssueKey "PROJ-123" -Raw
```

The script sends a GET request to the Jira issue endpoint using the `JIRA_APIKEY` environment variable as a bearer token.
