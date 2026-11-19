@@echo off
SET REPO_DIR=%~1
SET REPO_DIR=%REPO_DIR:"=%

cd /d "%REPO_DIR%"

echo Fetching the absolute newest binaries directly from GitHub...

:: 1. Forcefully remove the old Libs folder if it exists
if exist "Libs" (
    echo Cleaning up old local Libs...
    rd /s /q "Libs" 2>nul
    
    :: Safety check if Visual Studio or another process is locking a DLL
    if exist "Libs" (
        echo ERROR: Could not clear the Libs folder. Is a file open or running in Visual Studio?
        exit /b 1
    )
)

:: 2. Fresh clone the Shared-Libs repository directly into the 'Libs' folder
git clone --depth 1 https://github.com/David-plasmo/Shared-Libs Libs

if exist "Libs" (
    echo.
    echo Success! The Libs folder has been refreshed with its exact structure.
) else (
    echo.
    echo ERROR: Failed to clone from GitHub. Please check your network connection or permissions.
)

echo.
echo Done!
