Import-Module powerGate
$tenant = 'yourTenant'
$user = 'yourFlUser'
$password = 'yourFlPassword'
Disconnect-ERP
Connect-ERP -Service "http://localhost:8080/coolOrange/FL_SVC"
#login to your Fusion Lifecycle tenant
$login = Add-ERPObject -EntitySet "FlLogin" -Properties @{tenant=$tenant;user=$user;password=$password}
#get all FL workspaces
$workspaces = Get-ERPObjects -EntitySet "FlWorkspaces"
#get all items from the given workspace
$items = Get-ERPObjects -EntitySet "FlItems" -Filter "WorkspaceId eq 93"
#get item information, including properties, relations, and attachments
$item = Get-ERPObject -EntitySet "FlItems" -Keys @{WorkspaceId=93;Id=7660} -Expand @('Properties','Relations','Attachments')

#create a new item in the given workspace
$properties = @()
$properties += New-ERPObject -EntityType "FusionLifecycleItemProperty" -Properties @{Name='NUMBER';Value='CO-9999-998'}
$properties += New-ERPObject -EntityType "FusionLifecycleItemProperty" -Properties @{Name='TITLE';Value='coolOrange powerGate test'}
$newItem = New-ERPObject -EntityType "FusionLifecycleItem" -Properties @{WorkspaceId = 8;Properties=$properties}
$item = Add-ERPObject -EntitySet "FlItems" -Properties $newItem

#upload a file to the given workspace/item
Add-ERPMedia -EntitySet "FlFile" -File C:\temp\test.pdf -Properties @{WorkspaceId=93;Id=7660;FileName="test.pdf";Description="test upload from powerGate"}
