var OxObaf9=["onload","contentWindow","idSource","innerHTML","body","document","","designMode","on","contentEditable","fontFamily","style","Tahoma","fontSize","11px","color","black","background","white","length","\x22","\x3C$1$3"," ","\x26nbsp;","$1","\x3Ch$1\x3E","\x3C$1\x3E$2\x3C/$1\x3E"];var editor=Window_GetDialogArguments(window);function cancel(){Window_CloseDialog(window);} ;window[OxObaf9[0]]=function (){var iframe=document.getElementById(OxObaf9[2])[OxObaf9[1]];iframe[OxObaf9[5]][OxObaf9[4]][OxObaf9[3]]=OxObaf9[6];iframe[OxObaf9[5]][OxObaf9[7]]=OxObaf9[8];iframe[OxObaf9[5]][OxObaf9[4]][OxObaf9[9]]=true;iframe[OxObaf9[5]][OxObaf9[4]][OxObaf9[11]][OxObaf9[10]]=OxObaf9[12];iframe[OxObaf9[5]][OxObaf9[4]][OxObaf9[11]][OxObaf9[13]]=OxObaf9[14];iframe[OxObaf9[5]][OxObaf9[4]][OxObaf9[11]][OxObaf9[15]]=OxObaf9[16];iframe[OxObaf9[5]][OxObaf9[4]][OxObaf9[11]][OxObaf9[17]]=OxObaf9[18];iframe.focus();} ;function insertContent(){var iframe=document.getElementById(OxObaf9[2])[OxObaf9[1]];var Oxce=iframe[OxObaf9[5]][OxObaf9[4]][OxObaf9[3]];if(Oxce&&Oxce[OxObaf9[19]]>0){editor.PasteHTML(_RemoveWord(Oxce));Window_CloseDialog(window);} ;} ;function _RemoveWord(Ox2d){Ox2d=Ox2d.replace(/<[\/]?(base|meta|link|style|font|st1|shape|path|lock|imagedata|stroke|formulas|xml|del|ins|[ovwxp]:\w+)[^>]*?>/gi,OxObaf9[6]);Ox2d=Ox2d.replace(/\s*mso-[^:]+:[^;"]+;?/gi,OxObaf9[6]);Ox2d=Ox2d.replace(/<!--[\s\S]*?-->/gi,OxObaf9[6]);Ox2d=Ox2d.replace(/\s*MARGIN: 0cm 0cm 0pt\s*;/gi,OxObaf9[6]);Ox2d=Ox2d.replace(/\s*MARGIN: 0cm 0cm 0pt\s*"/gi,OxObaf9[20]);Ox2d=Ox2d.replace(/\s*TEXT-INDENT: 0cm\s*;/gi,OxObaf9[6]);Ox2d=Ox2d.replace(/\s*TEXT-INDENT: 0cm\s*"/gi,OxObaf9[20]);Ox2d=Ox2d.replace(/\s*TEXT-ALIGN: [^\s;]+;?"/gi,OxObaf9[20]);Ox2d=Ox2d.replace(/\s*PAGE-BREAK-BEFORE: [^\s;]+;?"/gi,OxObaf9[20]);Ox2d=Ox2d.replace(/\s*FONT-VARIANT: [^\s;]+;?"/gi,OxObaf9[20]);Ox2d=Ox2d.replace(/\s*tab-stops:[^;"]*;?/gi,OxObaf9[6]);Ox2d=Ox2d.replace(/\s*tab-stops:[^"]*/gi,OxObaf9[6]);Ox2d=Ox2d.replace(/<(\w[^>]*) class=([^ |>]*)([^>]*)/gi,OxObaf9[21]);Ox2d=Ox2d.replace(/\s*style="\s*"/gi,OxObaf9[6]);Ox2d=Ox2d.replace(/<SPAN\s*[^>]*>\s* \s*<\/SPAN>/gi,OxObaf9[22]);Ox2d=Ox2d.replace(/<(\w+)[^>]*\sstyle="[^"]*DISPLAY\s?:\s?none(.*?)<\/\1>/ig,OxObaf9[6]);Ox2d=Ox2d.replace(/<span\s*[^>]*>\s*&nbsp;\s*<\/span>/gi,OxObaf9[23]);Ox2d=Ox2d.replace(/<SPAN\s*[^>]*><\/SPAN>/gi,OxObaf9[6]);Ox2d=Ox2d.replace(/<(\w[^>]*) lang=([^ |>]*)([^>]*)/gi,OxObaf9[21]);Ox2d=Ox2d.replace(/<SPAN\s*>(.*?)<\/SPAN>/gi,OxObaf9[24]);Ox2d=Ox2d.replace(/<\/?\w+:[^>]*>/gi,OxObaf9[6]);Ox2d=Ox2d.replace(/<\!--.*?-->/g,OxObaf9[6]);Ox2d=Ox2d.replace(/<H\d>\s*<\/H\d>/gi,OxObaf9[6]);Ox2d=Ox2d.replace(/<(\w[^>]*) language=([^ |>]*)([^>]*)/gi,OxObaf9[21]);Ox2d=Ox2d.replace(/<(\w[^>]*) onmouseover="([^\"]*)"([^>]*)/gi,OxObaf9[21]);Ox2d=Ox2d.replace(/<(\w[^>]*) onmouseout="([^\"]*)"([^>]*)/gi,OxObaf9[21]);Ox2d=Ox2d.replace(/<H(\d)([^>]*)>/gi,OxObaf9[25]);Ox2d=Ox2d.replace(/<(H\d)><FONT[^>]*>(.*?)<\/FONT><\/\1>/gi,OxObaf9[26]);Ox2d=Ox2d.replace(/<(H\d)><EM>(.*?)<\/EM><\/\1>/gi,OxObaf9[26]);Ox2d=Ox2d.replace(/<a name="?OLE_LINK\d+"?>((.|[\r\n])*?)<\/a>/gi,OxObaf9[24]);Ox2d=Ox2d.replace(/<a name="?_Hlt\d+"?>((.|[\r\n])*?)<\/a>/gi,OxObaf9[24]);Ox2d=Ox2d.replace(/<a name="?_Toc\d+"?>((.|[\r\n])*?)<\/a>/gi,OxObaf9[24]);return Ox2d;} ;