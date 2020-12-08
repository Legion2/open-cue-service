# Open CUE Service [![latest release](https://img.shields.io/github/v/release/Legion2/open-cue-service)](https://github.com/Legion2/open-cue-service/releases/latest) [![Build](https://github.com/Legion2/open-cue-service/workflows/Build/badge.svg)](https://github.com/Legion2/open-cue-service/actions?query=workflow%3ABuild)
HTTP REST API service for [Open CUE CLI](https://github.com/Legion2/open-cue-cli).

## Getting Started
Download and extract the latest [release](https://github.com/Legion2/open-cue-service/releases).
Starting with iCUE 3.82.80 the iCUE profiles in the `C:\ProgramData\Corsair\CUE\GameSdkEffects\profiles` directory are used.
Create this directory or configure another one, for details on how to setup profiles see [Open CUE CLI documentation](https://github.com/Legion2/open-cue-cli#profiles).
Then execute the `open-cue-service.exe` to start the service.
It runs on [http://localhost:25555](http://localhost:25555) and can be accessed with any Rest/Http client.
By default the API is only available on localhost for security reasons, if you want to expose it in your network please setup a HTTP proxy with authentication such as nginx.

## API Documentation
The Rest API exposes an OpenAPI Document at `/openapi/v1/openapi.json`.
The API definition can be viewed in the interactive Swagger UI which is hosted at `/openapi`.
Just start the server and open [http://localhost:25555/openapi](http://localhost:25555/openapi).
You can also open the [online version of the API documentation](https://legion2.github.io/open-cue-service/) to explore it without running the server.

[![openapi](docs/img/openapi.png)](https://legion2.github.io/open-cue-service/)

## Configuration
The profiles directory can be changed, the default is `profiles`.
Change the property `"Game": "<your profile directory>"` in the `appsettings.json`.

The Auto Sync for automatic reconnect and detection of crashed iCUE must be enable in the configuration.
For the Auto Sync feature an additional profile is required, which must be in the selected profile directory.
The profile should be empty and should not have any lighting effect.
The profile is used by the service to check if iCUE is running and can be controlled.
Change the property `"AutoSyncProfileName": "<name of empty profile>"` in the `appsettings.json`.
When using the Auto Sync feature, for some effects the opacity value lower than 100% can cause lighting artifacts.

You can also configure the Auto Sync Interval, which is the time in seconds between the checks if iCUE is running.
Change the property `"AutoSyncInterval": <interval in seconds>` in the `appsettings.json`.

To activate a profile on startup you can set the property `"StartProfileName": "<profile name>"` to an existing profile you want to be active on start.
Default is an empty string, which means no profile will be activated on startup.

## Troubleshooting

### Sdk Error occurred: InvalidArguments

This error could indicate the missing or wrong profiles directory.
Make sure the profiles directory is correctly created according to the [Open CUE CLI documentation](https://github.com/Legion2/open-cue-cli#profiles) and the correct name is configured in the `appsettings.json` (`Game` property).

### Sdk Error occurred: ProfilesConfigurationProblem

When you get this error, make sure you have created the `priorities.cfg` file with all the entries correctly.
Make sure you only use allows character in the name of the profiles.
Reexport the profiles which the correct name, do not rename the profiles after you have exported them.
Restart iCUE and Open CUE Service after you have changed any file.

## License
This project is distributed under [Apache License, Version 2.0](LICENSE).

The software iCUE and the file CgSDK.x64_2015.dll are supplied by Corsair Components, Inc. and distributed under different terms.
For more information, contact Corsair Components, Inc.
