@@echo off
SET PROJECT_NAME=%~1
SET TARGET_CONFIG=%~3

:: Use standard batch modifiers to resolve the parent directory cleanly 
for %%I in ("%~2\..") do set REPO_DIR=%%~fI

echo ==============================================
echo PROJECT NAME:  %PROJECT_NAME%
echo REPO DIR:      %REPO_DIR%
echo CONFIG:        %TARGET_CONFIG%
echo ==============================================

echo [1/3] Cleaning and preparing Shared-Libs repository...
cd /d "%REPO_DIR%\Libs"

:: 💡 CLEAN FIRST: Clear out old junk before moving new files in
git reset --hard HEAD
git clean -fd

:: Ensure the destination directory exists inside the cloned Libs folder
if not exist "%REPO_DIR%\Libs\%PROJECT_NAME%\bin" mkdir "%REPO_DIR%\Libs\%PROJECT_NAME%\bin"

echo [2/3] Copying and Staging binaries...
echo Copying binaries from: %PROJECT_NAME%\bin\%TARGET_CONFIG%\
xcopy /Y "%REPO_DIR%\%PROJECT_NAME%\bin\%TARGET_CONFIG%\%PROJECT_NAME%.dll" "%REPO_DIR%\Libs\%PROJECT_NAME%\bin\"
xcopy /Y "%REPO_DIR%\%PROJECT_NAME%\bin\%TARGET_CONFIG%\%PROJECT_NAME%.pdb" "%REPO_DIR%\Libs\%PROJECT_NAME%\bin\"

:: Stage the newly copied binaries
git add "%PROJECT_NAME%/bin/%PROJECT_NAME%.dll" "%PROJECT_NAME%/bin/%PROJECT_NAME%.pdb"
if %ERRORLEVEL% NEQ 0 (echo ERROR: 'git add' failed. && exit /b 128)

:: Commit changes (using || verifications to handle nothing-to-commit cleanly)
git commit -m "Automated Build Update (%TARGET_CONFIG%): %PROJECT_NAME% binaries"
if %ERRORLEVEL% NEQ 0 (
    echo WARNING: 'git commit' skipped or failed. This usually means nothing changed.
)

:: Sync with cloud and push updates to the Shared-Libs repository
echo Syncing with GitHub...
git fetch origin main
if %ERRORLEVEL% NEQ 0 (echo ERROR: 'git fetch' failed. Check internet/permissions. && exit /b 128)

git rebase origin/main
if %ERRORLEVEL% NEQ 0 (echo ERROR: 'git rebase' failed. && exit /b 128)

echo Pushing to GitHub...
git push origin HEAD:main
if %ERRORLEVEL% NEQ 0 (echo ERROR: 'git push' failed. Check GitHub credentials. && exit /b 128)
