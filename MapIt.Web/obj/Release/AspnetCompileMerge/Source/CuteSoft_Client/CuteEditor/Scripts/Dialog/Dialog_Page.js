var OxOf057=["inp_title","inp_doctype","inp_description","inp_keywords","PageLanguage","HTMLEncoding","bgcolor","bgcolor_Preview","fontcolor","fontcolor_Preview","Backgroundimage","btnbrowse","TopMargin","BottomMargin","LeftMargin","RightMargin","MarginWidth","MarginHeight","btnok","btncc","editor","window","document","body","head","title","value","innerHTML","DOCTYPE","meta","length","name","keywords","content","description","httpEquiv","content-type","content-language","background","color","style","backgroundColor","bgColor","topMargin","bottomMargin","leftMargin","rightMargin","marginWidth","marginHeight","","[[ValidNumber]]","Please enter a valid color number.","text","childNodes","parentNode","http-equiv","Content-Type","Content-Language","META","onclick"];var inp_title=Window_GetElement(window,OxOf057[0],true);var inp_doctype=Window_GetElement(window,OxOf057[1],true);var inp_description=Window_GetElement(window,OxOf057[2],true);var inp_keywords=Window_GetElement(window,OxOf057[3],true);var PageLanguage=Window_GetElement(window,OxOf057[4],true);var HTMLEncoding=Window_GetElement(window,OxOf057[5],true);var bgcolor=Window_GetElement(window,OxOf057[6],true);var bgcolor_Preview=Window_GetElement(window,OxOf057[7],true);var fontcolor=Window_GetElement(window,OxOf057[8],true);var fontcolor_Preview=Window_GetElement(window,OxOf057[9],true);var Backgroundimage=Window_GetElement(window,OxOf057[10],true);var btnbrowse=Window_GetElement(window,OxOf057[11],true);var TopMargin=Window_GetElement(window,OxOf057[12],true);var BottomMargin=Window_GetElement(window,OxOf057[13],true);var LeftMargin=Window_GetElement(window,OxOf057[14],true);var RightMargin=Window_GetElement(window,OxOf057[15],true);var MarginWidth=Window_GetElement(window,OxOf057[16],true);var MarginHeight=Window_GetElement(window,OxOf057[17],true);var btnok=Window_GetElement(window,OxOf057[18],true);var btncc=Window_GetElement(window,OxOf057[19],true);var obj=Window_GetDialogArguments(window);var editor=obj[OxOf057[20]];var editwin=obj[OxOf057[21]];var editdoc=obj[OxOf057[22]];var body=editdoc[OxOf057[23]];var head=obj[OxOf057[24]];var title=head.getElementsByTagName(OxOf057[25])[0];if(title){inp_title[OxOf057[26]]=title[OxOf057[27]];} ;inp_doctype[OxOf057[26]]=obj[OxOf057[28]];var metas=head.getElementsByTagName(OxOf057[29]);for(var m=0;m<metas[OxOf057[30]];m++){if(metas[m][OxOf057[31]].toLowerCase()==OxOf057[32]){inp_keywords[OxOf057[26]]=metas[m][OxOf057[33]];} ;if(metas[m][OxOf057[31]].toLowerCase()==OxOf057[34]){inp_description[OxOf057[26]]=metas[m][OxOf057[33]];} ;if(metas[m][OxOf057[35]].toLowerCase()==OxOf057[36]){HTMLEncoding[OxOf057[26]]=metas[m][OxOf057[33]];} ;if(metas[m][OxOf057[35]].toLowerCase()==OxOf057[37]){PageLanguage[OxOf057[26]]=metas[m][OxOf057[33]];} ;} ;if(editdoc[OxOf057[23]][OxOf057[38]]){Backgroundimage[OxOf057[26]]=editdoc[OxOf057[23]][OxOf057[38]];} ;if(editdoc[OxOf057[23]][OxOf057[40]][OxOf057[39]]){fontcolor[OxOf057[26]]=editdoc[OxOf057[23]][OxOf057[40]][OxOf057[39]];fontcolor[OxOf057[40]][OxOf057[41]]=fontcolor[OxOf057[26]];fontcolor_Preview[OxOf057[40]][OxOf057[41]]=fontcolor[OxOf057[26]];} ;var body_bgcolor=editdoc[OxOf057[23]][OxOf057[40]][OxOf057[41]]||editdoc[OxOf057[23]][OxOf057[42]];if(body_bgcolor){bgcolor[OxOf057[26]]=body_bgcolor;bgcolor[OxOf057[40]][OxOf057[41]]=body_bgcolor;bgcolor_Preview[OxOf057[40]][OxOf057[41]]=body_bgcolor;} ;if(Browser_IsWinIE()){if(body[OxOf057[43]]){TopMargin[OxOf057[26]]=body[OxOf057[43]];} ;if(body[OxOf057[44]]){BottomMargin[OxOf057[26]]=body[OxOf057[44]];} ;if(body[OxOf057[45]]){LeftMargin[OxOf057[26]]=body[OxOf057[45]];} ;if(body[OxOf057[46]]){RightMargin[OxOf057[26]]=body[OxOf057[46]];} ;if(body[OxOf057[47]]){MarginWidth[OxOf057[26]]=body[OxOf057[47]];} ;if(body[OxOf057[48]]){MarginHeight[OxOf057[26]]=body[OxOf057[48]];} ;} else {if(body.getAttribute(OxOf057[43])){TopMargin[OxOf057[26]]=body.getAttribute(OxOf057[43]);} ;if(body.getAttribute(OxOf057[44])){BottomMargin[OxOf057[26]]=body.getAttribute(OxOf057[44]);} ;if(body.getAttribute(OxOf057[45])){LeftMargin[OxOf057[26]]=body.getAttribute(OxOf057[45]);} ;if(body.getAttribute(OxOf057[46])){RightMargin[OxOf057[26]]=body.getAttribute(OxOf057[46]);} ;if(body.getAttribute(OxOf057[16])){MarginWidth[OxOf057[26]]=body.getAttribute(OxOf057[47]);} ;if(body.getAttribute(OxOf057[48])){MarginHeight[OxOf057[26]]=body.getAttribute(OxOf057[48]);} ;} ;function do_insert(){try{if(Browser_IsWinIE()){body[OxOf057[43]]=TopMargin[OxOf057[26]];body[OxOf057[44]]=BottomMargin[OxOf057[26]];body[OxOf057[45]]=LeftMargin[OxOf057[26]];body[OxOf057[46]]=RightMargin[OxOf057[26]];if(MarginWidth[OxOf057[26]]){body[OxOf057[47]]=MarginWidth[OxOf057[26]];} ;if(MarginHeight[OxOf057[26]]){body[OxOf057[48]]=MarginHeight[OxOf057[26]];} ;} else {body.setAttribute(OxOf057[43],TopMargin.value);body.setAttribute(OxOf057[44],BottomMargin.value);body.setAttribute(OxOf057[45],LeftMargin.value);body.setAttribute(OxOf057[46],RightMargin.value);body.setAttribute(OxOf057[47],MarginWidth.value);body.setAttribute(OxOf057[48],MarginHeight.value);if(body.getAttribute(OxOf057[43])==OxOf057[49]){body.removeAttribute(OxOf057[43]);} ;if(body.getAttribute(OxOf057[44])==OxOf057[49]){body.removeAttribute(OxOf057[44]);} ;if(body.getAttribute(OxOf057[45])==OxOf057[49]){body.removeAttribute(OxOf057[45]);} ;if(body.getAttribute(OxOf057[46])==OxOf057[49]){body.removeAttribute(OxOf057[46]);} ;if(body.getAttribute(OxOf057[47])==OxOf057[49]){body.removeAttribute(OxOf057[47]);} ;if(body.getAttribute(OxOf057[48])==OxOf057[49]){body.removeAttribute(OxOf057[48]);} ;} ;} catch(er){alert(OxOf057[50]);return ;} ;try{editdoc[OxOf057[23]][OxOf057[40]][OxOf057[41]]=bgcolor[OxOf057[26]];editdoc[OxOf057[23]][OxOf057[40]][OxOf057[39]]=fontcolor[OxOf057[26]];if(Backgroundimage[OxOf057[26]]){editdoc[OxOf057[23]][OxOf057[38]]=Backgroundimage[OxOf057[26]];} else {body.removeAttribute(OxOf057[38]);} ;} catch(er){alert(OxOf057[51]);return ;} ;if(!title){title=document.createElement(OxOf057[25]);head.appendChild(title);} ;if(Browser_IsWinIE()){title[OxOf057[52]]=inp_title[OxOf057[26]];} else {var Ox462=document.createTextNode(inp_title.value);try{title.removeChild(title[OxOf057[53]].item(0));} catch(x){} ;title.appendChild(Ox462);} ;for(var m=0;m<metas[OxOf057[30]];m++){var Oxb7=metas[m];if(Oxb7){if(Oxb7[OxOf057[31]].toLowerCase()==OxOf057[32]||Oxb7[OxOf057[31]].toLowerCase()==OxOf057[34]||Oxb7[OxOf057[31]].toLowerCase()==OxOf057[36]||Oxb7[OxOf057[31]].toLowerCase()==OxOf057[37]){Oxb7[OxOf057[54]].removeChild(Oxb7);Oxb7=null;} ;} ;} ;try{Ox463(OxOf057[31],OxOf057[32],inp_keywords.value);Ox463(OxOf057[31],OxOf057[34],inp_description.value);Ox463(OxOf057[55],OxOf057[56],HTMLEncoding.value);Ox463(OxOf057[55],OxOf057[57],PageLanguage.value);} catch(x){} ;function Ox463(Ox464,name,Oxce){var metas=head.getElementsByTagName(OxOf057[29]);for(var m=0;m<metas[OxOf057[30]];m++){if(metas[m][OxOf057[31]].toLowerCase()==name.toLowerCase()){metas[m][OxOf057[54]].removeChild(metas[m]);} ;} ;if(Oxce!=OxOf057[49]&&Oxce!=null){var Ox465=editdoc.createElement(OxOf057[58]);Ox465.setAttribute(Ox464,name);Ox465.setAttribute(OxOf057[33],Oxce);head.appendChild(Ox465);} ;} ;Window_SetDialogReturnValue(window,inp_doctype[OxOf057[26]]+OxOf057[49]);Window_CloseDialog(window);} ;btnbrowse[OxOf057[59]]=function btnbrowse_onclick(){function Ox35d(Ox13e){if(Ox13e){Backgroundimage[OxOf057[26]]=Ox13e;FireUIChanged();} ;} ;editor.SetNextDialogWindow(window);if(Browser_IsSafari()){editor.ShowSelectImageDialog(Ox35d,Backgroundimage.value,Backgroundimage);} else {editor.ShowSelectImageDialog(Ox35d,Backgroundimage.value);} ;} ;function do_Close(){Window_CloseDialog(window);} ;fontcolor[OxOf057[59]]=fontcolor_Preview[OxOf057[59]]=function fontcolor_onclick(){SelectColor(fontcolor,fontcolor_Preview);} ;bgcolor[OxOf057[59]]=bgcolor_Preview[OxOf057[59]]=function bgcolor_onclick(){SelectColor(bgcolor,bgcolor_Preview);} ;