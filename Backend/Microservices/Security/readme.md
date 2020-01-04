<h2>Generate certificate for debug</h2>

Open the Develop Command Prompt for VS
<code>
makecert -n "CN=PatchaSecurity" -len 2048 -a sha256 -sv PatchaSecurity.pvk -r PatchaSecurity.cer
</code>
<code>
pvk2pfx -pvk PatchaSecurity.pvk -spc PatchaSecurity.cer -pfx PatchaSecurity.pfx
</code>

reference: https://devblogs.microsoft.com/aspnet/asp-net-core-authentication-with-identityserver4/