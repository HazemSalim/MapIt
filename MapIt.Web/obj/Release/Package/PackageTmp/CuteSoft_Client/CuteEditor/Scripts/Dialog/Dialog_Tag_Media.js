var OxOdf84=["outer","btnbrowse","inp_src","onclick","value","cssText","style","src","FileName"];var outer=Window_GetElement(window,OxOdf84[0],true);var btnbrowse=Window_GetElement(window,OxOdf84[1],true);var inp_src=Window_GetElement(window,OxOdf84[2],true);btnbrowse[OxOdf84[3]]=function btnbrowse_onclick(){function Ox35d(Ox13e){if(Ox13e){inp_src[OxOdf84[4]]=Ox13e;} ;} ;editor.SetNextDialogWindow(window);editor.ShowSelectFileDialog(Ox35d,inp_src.value);} ;UpdateState=function UpdateState_Media(){outer[OxOdf84[6]][OxOdf84[5]]=element[OxOdf84[6]][OxOdf84[5]];outer.mergeAttributes(element);if(element[OxOdf84[7]]){outer[OxOdf84[8]]=element[OxOdf84[8]];} else {outer.removeAttribute(OxOdf84[8]);} ;} ;SyncToView=function SyncToView_Media(){inp_src[OxOdf84[4]]=element[OxOdf84[8]];} ;SyncTo=function SyncTo_Media(element){element[OxOdf84[8]]=inp_src[OxOdf84[4]];} ;