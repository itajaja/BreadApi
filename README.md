BreadApi
========

Web service to automatically expose any SIMATIC IT 6.5 Breads as (*quasi*)REST service.

Set up
-------------
The BreadApi WebService is built using MVC 4 WEBAPI. It has to run on a machine with SimaticIT installed.

Configuration
-------------
In order to use a specific bread the Web.config file must be edited as following:
In the `appSettings` section modify the following values:

- `PathToSit`: The path to the root installation of SIMATIC IT. Example:

        C:\Program Files (x86)\Siemens\SIT\
- `BreadDlls`: The path to the required bread dlls from the `PathToSit` Folder, separated by comma. Example:

        MES\BIN\OEEBread.dll,MES\BIN\OEEBread.dll,MES\BIN\DMBread.dll,MES\BIN\MMread.dll

Running
-------------
