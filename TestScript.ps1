Import-Module powerGate
cls
Disconnect-ERP
Connect-ERP -Service "http://w10-2019-demo:8080/coolOrange/FL_SVC"
$login = Add-ERPObject -EntitySet "FlLogin" -Properties @{tenant='coolorange';user='marco.mirandola@coolorange.com';password='i40Ladroni'}
$workspaces = Get-ERPObjects -EntitySet "FlWorkspaces"
$workspaces.Label
$items = Get-ERPObjects -EntitySet "FlItems" -Filter "WorkspaceId eq 8"
$item = Get-ERPObject -EntitySet "FlItems" -Keys @{WorkspaceId=8;Id=278} -Expand @('Properties','Relations')



$properties = @()
$properties += New-ERPObject -EntityType "FusionLifecycleItemProperty" -Properties @{Name='NUMBER';Value='CO-9999-998'}
$properties += New-ERPObject -EntityType "FusionLifecycleItemProperty" -Properties @{Name='TITLE';Value='coolOrange powerGate test'}
$newItem = New-ERPObject -EntityType "FusionLifecycleItem" -Properties @{WorkspaceId = 8;Properties=$properties}
Add-ERPObject -EntitySet "FlItems" -Properties $newItem

$itemWs = $workspaces | Where-Object { $_.Label -eq "CO_TEST_WS"}
$itemWs
