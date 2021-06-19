mkdir ..\git\hooks

xcopy pre-commit ..\.git\hooks\pre-commit /y
xcopy post-checkout ..\.git\hooks\post-checkout /y
xcopy post-merge ..\.git\hooks\post-merge /y