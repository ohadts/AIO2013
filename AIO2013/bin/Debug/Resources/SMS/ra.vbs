ipstring = WScript.Arguments(0) 
set shell = createobject("wscript.shell" ) 
shell.run "hcp://CN=Microsoft%20Corporation,L=Redmond,S=Washington,C=US/Remote%20Assistance/Escalation/Unsolicited/unsolicitedrcui.htm" 
shell.AppActivate("Help and Support Center" ) 
wscript.sleep 2000 
shell.sendkeys "{TAB 9}" 
shell.sendkeys ipstring 
wscript.sleep 500 
shell.sendkeys "%c"
wscript.sleep 500 
shell.sendkeys "%s"
