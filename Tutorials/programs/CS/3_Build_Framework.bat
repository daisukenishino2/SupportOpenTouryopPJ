setlocal

@rem --------------------------------------------------
@rem Turn off the echo function.
@rem --------------------------------------------------
@echo off

@rem --------------------------------------------------
@rem Get the path to the executable file.
@rem --------------------------------------------------
set CURRENT_DIR="%~dp0"

@rem --------------------------------------------------
@rem Execution of the common processing.
@rem --------------------------------------------------
call %CURRENT_DIR%z_Common.bat

rem --------------------------------------------------
rem Make the Directory.
rem --------------------------------------------------
md "Frameworks\Infrastructure\Temp"
md "Frameworks\Infrastructure\Build"

rem --------------------------------------------------
rem Output xcopy after you build the batch Infrastructure(AllComponent)
rem --------------------------------------------------

..\nuget.exe restore "Frameworks\Infrastructure\AllComponent.sln"
%BUILDFILEPATH% %COMMANDLINE% "Frameworks\Infrastructure\AllComponent.sln"

xcopy /E /Y "Frameworks\Infrastructure\Business\bin\%BUILD_CONFIG%" "Frameworks\Infrastructure\Temp\%BUILD_CONFIG%\"
xcopy /E /Y "Frameworks\Infrastructure\CustomControl\bin\%BUILD_CONFIG%" "Frameworks\Infrastructure\Temp\%BUILD_CONFIG%\"

xcopy /E /Y "Frameworks\Infrastructure\Temp\%BUILD_CONFIG%" "Frameworks\Infrastructure\Build\"

pause

rem -------------------------------------------------------
endlocal
