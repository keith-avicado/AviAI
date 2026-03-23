[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string]$BaseUrl,

    [Parameter(Mandatory = $true)]
    [string]$IssueKey,

    [switch]$Raw
)

$apiKey = $env:JIRA_APIKEY

if ([string]::IsNullOrWhiteSpace($apiKey)) {
    throw "The JIRA_APIKEY environment variable must be set before reading a Jira ticket."
}

$normalizedBaseUrl = $BaseUrl.TrimEnd("/")
$issueUri = "{0}/rest/api/latest/issue/{1}" -f $normalizedBaseUrl, [Uri]::EscapeDataString($IssueKey)
$headers = @{
    Authorization = "Bearer $apiKey"
    Accept = "application/json"
}

$response = Invoke-RestMethod -Method Get -Uri $issueUri -Headers $headers

if ($Raw.IsPresent) {
    $response
    return
}

$fields = $response.fields

[pscustomobject]@{
    Key = $response.key
    Summary = $fields.summary
    Status = $fields.status.name
    IssueType = $fields.issuetype.name
    Priority = $fields.priority.name
    Assignee = $fields.assignee.displayName
    Reporter = $fields.reporter.displayName
    Updated = $fields.updated
    Url = "{0}/browse/{1}" -f $normalizedBaseUrl, $response.key
}
