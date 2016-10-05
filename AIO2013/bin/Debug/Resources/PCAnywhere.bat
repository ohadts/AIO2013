Set App_Working_Dir="C:\\Program Files\\Symantec\\pcAnywhere"
Set App_PE_Name=AWREM32.EXE
Set App_Doc_Name="%~dp0\Net.chf"
pushd %App_Working_Dir%
start %App_PE_Name% %App_Doc_Name% /C%1
popd
