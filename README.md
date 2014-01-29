BreadApi
========

Web service to automatically expose any SIMATIC IT 6.5 Breads as (*quasi*)REST service.

Set up
-------------
The BreadApi WebService is built using MVC 4 WEBAPI. It has to run on a machine with SimaticIT installed. the service has been tested with SIMATIC IT 6.5. While it might work also with other versions, these are not currently supported.

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
The API are exposed as `POST` requests at the address `breads/{breadPackage}/{bread}/{action}` where:

- `breadPackage` is the name of the Bread package (e.g. `OEE`, `MM`, `POM`)
- `bread` is the name of the bread instance (e.g. `Algorithm`, `MachineState`)
- `action` is the name of the method to request (e.g. `create`, `select`, `Edit`)

For example, if I want to call the Select method in the Equipment bread of the OEEBread.dll, the url would

    breads/oee/equipment/select

All the available api calls can be navigated from the home page.

Currently the following methods can be called (if the specified bread support them)

- `Select`
- `Create` (To be implemented)
- `Delete` (To be implemented)
- `Edit` (To be implemented)
- `SelectCount` (To be implemented)
- `SelectByPK` (To be implemented)
- `SelectRank` (To be implemented)
- `SetCurrentTransationHandle` (To be implemented)
- `UnsetCurrentTransationHandle` (To be implemented)
- `SetCurrentUser` (To be implemented)

Refer to the guide `OEEBREADENG.chm` for the documentation for these methods.
Custom Methods defined by each API for now are not supported.

