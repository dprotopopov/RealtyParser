rem Create a single .dll that combines the all subassemblies.
ren "My*.dll" "*.dll.temp"
ren "RT*.dll" "*.dll.temp"
ren "EntityFramework*.dll" "*.dll.temp"
ren "ICSharpCode*.dll" "*.dll.temp"
ren "ServiceStack*.dll" "*.dll.temp"
ren "System*.dll" "*.dll.temp"
rem ren "libtidy.dll" "*.dll.temp"
ren "TidyManaged.dll" "*.dll.temp"
ren "HtmlAgilityPack.dll" "*.dll.temp"
ren "RealtyParser*.dll" "*.dll.temp"
"..\..\..\ILMerge.exe" /out:"RealtyParser.dll" "*.dll.temp" /target:library /targetplatform:v4,C:\Windows\Microsoft.NET\Framework64\v4.0.30319 /wildcards /zeroPeKind
del *.dll.temp
exit 0