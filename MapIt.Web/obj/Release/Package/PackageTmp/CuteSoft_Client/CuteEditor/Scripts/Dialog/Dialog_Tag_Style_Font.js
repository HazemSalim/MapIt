var OxOed79=["SetStyle","length","","GetStyle","GetText",":",";","cssText","sel_font","div_font_detail","sel_fontfamily","cb_decoration_under","cb_decoration_over","cb_decoration_through","cb_style_bold","cb_style_italic","sel_fontTransform","sel_fontsize","inp_fontsize","sel_fontsize_unit","inp_color","inp_color_Preview","outer","div_demo","disabled","selectedIndex","style","value","font","fontFamily","color","backgroundColor","textDecoration","checked","overline","underline","line-through","fontWeight","bold","fontStyle","italic","fontSize","options","textTransform","font-family","overline ","underline ","line-through ","onclick"];function pause(Ox4a3){var Oxa8= new Date();var Ox4a4=Oxa8.getTime()+Ox4a3;while(true){Oxa8= new Date();if(Oxa8.getTime()>Ox4a4){return ;} ;} ;} ;function StyleClass(Ox201){var Ox4a6=[];var Ox4a7={};if(Ox201){Ox4ac();} ;this[OxOed79[0]]=function SetStyle(name,Ox4f,Ox4a9){name=name.toLowerCase();for(var i=0;i<Ox4a6[OxOed79[1]];i++){if(Ox4a6[i]==name){break ;} ;} ;Ox4a6[i]=name;Ox4a7[name]=Ox4f?(Ox4f+(Ox4a9||OxOed79[2])):OxOed79[2];} ;this[OxOed79[3]]=function GetStyle(name){name=name.toLowerCase();return Ox4a7[name]||OxOed79[2];} ;this[OxOed79[4]]=function Ox4ab(){var Ox201=OxOed79[2];for(var i=0;i<Ox4a6[OxOed79[1]];i++){var Ox27=Ox4a6[i];var p=Ox4a7[Ox27];if(p){Ox201+=Ox27+OxOed79[5]+p+OxOed79[6];} ;} ;return Ox201;} ;function Ox4ac(){var arr=Ox201.split(OxOed79[6]);for(var i=0;i<arr[OxOed79[1]];i++){var p=arr[i].split(OxOed79[5]);var Ox27=p[0].replace(/^\s+/g,OxOed79[2]).replace(/\s+$/g,OxOed79[2]).toLowerCase();Ox4a6[Ox4a6[OxOed79[1]]]=Ox27;Ox4a7[Ox27]=p[1];} ;} ;} ;function GetStyle(Ox137,name){return  new StyleClass(Ox137.cssText).GetStyle(name);} ;function SetStyle(Ox137,name,Ox4f,Ox4ad){var Ox4ae= new StyleClass(Ox137.cssText);Ox4ae.SetStyle(name,Ox4f,Ox4ad);Ox137[OxOed79[7]]=Ox4ae.GetText();} ;function ParseFloatToString(Ox24){var Ox8=parseFloat(Ox24);if(isNaN(Ox8)){return OxOed79[2];} ;return Ox8+OxOed79[2];} ;var sel_font=Window_GetElement(window,OxOed79[8],true);var div_font_detail=Window_GetElement(window,OxOed79[9],true);var sel_fontfamily=Window_GetElement(window,OxOed79[10],true);var cb_decoration_under=Window_GetElement(window,OxOed79[11],true);var cb_decoration_over=Window_GetElement(window,OxOed79[12],true);var cb_decoration_through=Window_GetElement(window,OxOed79[13],true);var cb_style_bold=Window_GetElement(window,OxOed79[14],true);var cb_style_italic=Window_GetElement(window,OxOed79[15],true);var sel_fontTransform=Window_GetElement(window,OxOed79[16],true);var sel_fontsize=Window_GetElement(window,OxOed79[17],true);var inp_fontsize=Window_GetElement(window,OxOed79[18],true);var sel_fontsize_unit=Window_GetElement(window,OxOed79[19],true);var inp_color=Window_GetElement(window,OxOed79[20],true);var inp_color_Preview=Window_GetElement(window,OxOed79[21],true);var outer=Window_GetElement(window,OxOed79[22],true);var div_demo=Window_GetElement(window,OxOed79[23],true);UpdateState=function UpdateState_Font(){inp_fontsize[OxOed79[24]]=sel_fontsize_unit[OxOed79[24]]=(sel_fontsize[OxOed79[25]]>0);div_font_detail[OxOed79[24]]=sel_font[OxOed79[25]]>0;div_demo[OxOed79[26]][OxOed79[7]]=element[OxOed79[26]][OxOed79[7]];} ;SyncToView=function SyncToView_Font(){sel_font[OxOed79[27]]=element[OxOed79[26]][OxOed79[28]].toLowerCase()||null;sel_fontfamily[OxOed79[27]]=element[OxOed79[26]][OxOed79[29]];inp_color[OxOed79[27]]=element[OxOed79[26]][OxOed79[30]];inp_color[OxOed79[26]][OxOed79[31]]=inp_color[OxOed79[27]];var Ox5e4=element[OxOed79[26]][OxOed79[32]].toLowerCase();cb_decoration_over[OxOed79[33]]=Ox5e4.indexOf(OxOed79[34])!=-1;cb_decoration_under[OxOed79[33]]=Ox5e4.indexOf(OxOed79[35])!=-1;cb_decoration_through[OxOed79[33]]=Ox5e4.indexOf(OxOed79[36])!=-1;cb_style_bold[OxOed79[33]]=element[OxOed79[26]][OxOed79[37]]==OxOed79[38];cb_style_italic[OxOed79[33]]=element[OxOed79[26]][OxOed79[39]]==OxOed79[40];sel_fontsize[OxOed79[27]]=element[OxOed79[26]][OxOed79[41]];sel_fontsize_unit[OxOed79[25]]=0;if(sel_fontsize[OxOed79[25]]==-1){if(ParseFloatToString(element[OxOed79[26]].fontSize)){sel_fontsize[OxOed79[27]]=ParseFloatToString(element[OxOed79[26]].fontSize);for(var i=0;i<sel_fontsize_unit[OxOed79[42]][OxOed79[1]];i++){var Ox142=sel_fontsize_unit.options(i)[OxOed79[27]];if(Ox142&&element[OxOed79[26]][OxOed79[41]].indexOf(Ox142)!=-1){sel_fontsize_unit[OxOed79[25]]=i;break ;} ;} ;} ;} ;sel_fontTransform[OxOed79[27]]=element[OxOed79[26]][OxOed79[43]];} ;SyncTo=function SyncTo_Font(element){SetStyle(element.style,OxOed79[28],sel_font.value);if(sel_fontfamily[OxOed79[27]]){element[OxOed79[26]][OxOed79[29]]=sel_fontfamily[OxOed79[27]];} else {SetStyle(element.style,OxOed79[44],OxOed79[2]);} ;try{element[OxOed79[26]][OxOed79[30]]=inp_color[OxOed79[27]]||OxOed79[2];} catch(x){element[OxOed79[26]][OxOed79[30]]=OxOed79[2];} ;var Ox5e6=cb_decoration_over[OxOed79[33]];var Ox5e7=cb_decoration_under[OxOed79[33]];var Ox5e8=cb_decoration_through[OxOed79[33]];if(!Ox5e6&&!Ox5e7&&!Ox5e8){element[OxOed79[26]][OxOed79[32]]=OxOed79[2];} else {var Ox58=OxOed79[2];if(Ox5e6){Ox58+=OxOed79[45];} ;if(Ox5e7){Ox58+=OxOed79[46];} ;if(Ox5e8){Ox58+=OxOed79[47];} ;element[OxOed79[26]][OxOed79[32]]=Ox58.substr(0,Ox58[OxOed79[1]]-1);} ;element[OxOed79[26]][OxOed79[37]]=cb_style_bold[OxOed79[33]]?OxOed79[38]:OxOed79[2];element[OxOed79[26]][OxOed79[39]]=cb_style_italic[OxOed79[33]]?OxOed79[40]:OxOed79[2];element[OxOed79[26]][OxOed79[43]]=sel_fontTransform[OxOed79[27]]||OxOed79[2];if(sel_fontsize[OxOed79[25]]>0){element[OxOed79[26]][OxOed79[41]]=sel_fontsize[OxOed79[27]];} else {if(ParseFloatToString(inp_fontsize.value)){element[OxOed79[26]][OxOed79[41]]=ParseFloatToString(inp_fontsize.value)+sel_fontsize_unit[OxOed79[27]];} else {element[OxOed79[26]][OxOed79[41]]=OxOed79[2];} ;} ;} ;inp_color[OxOed79[48]]=inp_color_Preview[OxOed79[48]]=function inp_color_onclick(){SelectColor(inp_color,inp_color_Preview);} ;