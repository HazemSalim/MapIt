var OxO4792=["Xml","Brushes","sh","CssClass","dp-xml","Style",".dp-xml .cdata { color: #ff1493; }",".dp-xml .tag, .dp-xml .tag-name { color: #069; font-weight: bold; }",".dp-xml .attribute { color: red; }",".dp-xml .attribute-value { color: blue; }","prototype","Aliases","xml","xhtml","xslt","html","ProcessRegexList","length","(\x26lt;|\x3C)\x5C!\x5C[[\x5Cw\x5Cs]*?\x5C[(.|\x5Cs)*?\x5C]\x5C](\x26gt;|\x3E)","gm","cdata","(\x26lt;|\x3C)!--\x5Cs*.*?\x5Cs*--(\x26gt;|\x3E)","comments","([:\x5Cw-.]+)\x5Cs*=\x5Cs*(\x22.*?\x22|\x27.*?\x27|\x5Cw+)*|(\x5Cw+)","attribute","index","attribute-value","(\x26lt;|\x3C)/*\x5C?*(?!\x5C!)|/*\x5C?*(\x26gt;|\x3E)","tag","(?:\x26lt;|\x3C)/*\x5C?*\x5Cs*([:\x5Cw-.]+)","tag-name"];dp[OxO4792[2]][OxO4792[1]][OxO4792[0]]=function (){this[OxO4792[3]]=OxO4792[4];this[OxO4792[5]]=OxO4792[6]+OxO4792[7]+OxO4792[8]+OxO4792[9];} ;dp[OxO4792[2]][OxO4792[1]][OxO4792[0]][OxO4792[10]]= new dp[OxO4792[2]].Highlighter();dp[OxO4792[2]][OxO4792[1]][OxO4792[0]][OxO4792[11]]=[OxO4792[12],OxO4792[13],OxO4792[14],OxO4792[15],OxO4792[13]];dp[OxO4792[2]][OxO4792[1]][OxO4792[0]][OxO4792[10]][OxO4792[16]]=function (){function Oxb7c(Oxb7d,Ox4f){Oxb7d[Oxb7d[OxO4792[17]]]=Ox4f;} ;var Ox1fc=0;var Ox93c=null;var Oxb7e=null;this.GetMatches( new RegExp(OxO4792[18],OxO4792[19]),OxO4792[20]);this.GetMatches( new RegExp(OxO4792[21],OxO4792[19]),OxO4792[22]);Oxb7e= new RegExp(OxO4792[23],OxO4792[19]);while((Ox93c=Oxb7e.exec(this.code))!=null){if(Ox93c[1]==null){continue ;} ;Oxb7c(this.matches, new dp[OxO4792[2]].Match(Ox93c[1],Ox93c.index,OxO4792[24]));if(Ox93c[2]!=undefined){Oxb7c(this.matches, new dp[OxO4792[2]].Match(Ox93c[2],Ox93c[OxO4792[25]]+Ox93c[0].indexOf(Ox93c[2]),OxO4792[26]));} ;} ;this.GetMatches( new RegExp(OxO4792[27],OxO4792[19]),OxO4792[28]);Oxb7e= new RegExp(OxO4792[29],OxO4792[19]);while((Ox93c=Oxb7e.exec(this.code))!=null){Oxb7c(this.matches, new dp[OxO4792[2]].Match(Ox93c[1],Ox93c[OxO4792[25]]+Ox93c[0].indexOf(Ox93c[1]),OxO4792[30]));} ;} ;