<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  version="1.0">
  <xsl:template match="/">


<H1 ALIGN="CENTER">Моделирование механической системы подвижного носителя</H1>
  <H2 ALIGN="CENTER">Начальные данные</H2>
           <xsl:for-each select="program/initialdata/group"> 
  <H3 ALIGN="LEFT"><xsl:value-of select="description"/></H3>
        <TABLE BORDER="1" CELLPADDING="3" cellspacing="0">
           <xsl:for-each select="var"> 
             <TR>  
              <TD>
                  <xsl:value-of select="@name"/>
                  <xsl:text>, </xsl:text> 
                  <xsl:value-of select="structure/unit"/>
              </TD>    
               <TD>          
               <xsl:for-each select="structure/data">
                  <xsl:value-of select="."/> 
                  <xsl:text> </xsl:text> 
               </xsl:for-each>  
              </TD>   
            </TR>           
            </xsl:for-each>
        </TABLE>
           <xsl:for-each select="group"> 
  <H4 ALIGN="LEFT"><xsl:value-of select="description"/></H4>
        <TABLE BORDER="1" CELLPADDING="3" cellspacing="0">
           <xsl:for-each select="var"> 
             <TR>  
              <TD>
                  <xsl:value-of select="@name"/>
                  <xsl:text>, </xsl:text> 
                  <xsl:value-of select="structure/unit"/>
              </TD>    
               <TD>          
               <xsl:for-each select="structure/data">
                  <xsl:value-of select="."/> 
                  <xsl:text> </xsl:text> 
               </xsl:for-each>  
              </TD>   
            </TR>           
            </xsl:for-each>
        </TABLE>		   
            </xsl:for-each>
            </xsl:for-each>
    <H2 ALIGN="CENTER">Результаты расчета</H2>
      <TABLE WIDTH="3500" BORDER="1" CELLPADDING="3" cellspacing="0">
         <THEAD>
            <TH>t, с</TH>
            <TH>X0_0, м</TH>
            <TH>X0_1, м </TH>
            <TH>X0_2, м </TH>
            <TH>X0_3, м </TH>
            <TH>X0_4, м </TH>
            <TH>X0_5, м </TH>
            <TH>X0_6, м </TH>    
            <TH>X0_7, м </TH>
            <TH>X0_8, м </TH>
            <TH>X0_9, м </TH>
            <TH>X0_10, м </TH>
            <TH>X0_11, м </TH>
            <TH>V_0, м/c </TH>
            <TH>V_1, м/c </TH> 
            <TH>V_2, м/c </TH> 
            <TH>Om_0, рад/c </TH>
            <TH>Om_1, рад/c </TH> 
            <TH>Om_2, рад/c </TH> 
            <TH>Vc_0, м/c </TH>
            <TH>Vc_1, м/c </TH> 
            <TH>Vc_2, м/c </TH> 
            <TH>AmpR_00, м/c </TH>
            <TH>AmpR_01, м/c </TH> 
            <TH>AmpR_02, м/c </TH> 
            <TH>AmpR_10, м/c </TH>
            <TH>AmpR_11, м/c </TH> 
            <TH>AmpR_12, м/c </TH> 
            <TH>AmpR_20, м/c </TH>
            <TH>AmpR_21, м/c </TH> 
            <TH>AmpR_22, м/c </TH> 
            <TH>AmpR_30, м/c </TH>
            <TH>AmpR_31, м/c </TH> 
            <TH>AmpR_32, м/c </TH> 
            <TH>AmpR_40, м/c </TH>
            <TH>AmpR_41, м/c </TH> 
            <TH>AmpR_42, м/c </TH> 
            <TH>AmpR_50, м/c </TH>
            <TH>AmpR_51, м/c </TH> 
            <TH>AmpR_52, м/c </TH> 
            <TH>teta, град </TH>
            <TH>psy, град </TH> 
            <TH>fi, град </TH> 

         </THEAD>
         <xsl:for-each select="program/resultdata/structure/values/record">
            <TR ALIGN="CENTER">
               <TD>
                  <xsl:value-of select="t"/>
               </TD>
               <TD>
                  <xsl:value-of select="X0_0"/>
               </TD>
               <TD>
                  <xsl:value-of select="X0_1"/>
               </TD>
               <TD>
                  <xsl:value-of select="X0_2"/>
               </TD>
               <TD>
                  <xsl:value-of select="X0_3"/>
               </TD>
               <TD>
                  <xsl:value-of select="X0_4"/>
               </TD>
               <TD>
                  <xsl:value-of select="X0_5"/>
               </TD>
               <TD>
                  <xsl:value-of select="X0_6"/>
               </TD>
               <TD>
                  <xsl:value-of select="X0_7"/>
               </TD>
               <TD>
                  <xsl:value-of select="X0_8"/>
               </TD>
               <TD>
                  <xsl:value-of select="X0_9"/>
               </TD>
               <TD>
                  <xsl:value-of select="X0_10"/>
               </TD>
               <TD>
                  <xsl:value-of select="X0_11"/>
               </TD>
               <TD>
                  <xsl:value-of select="V_0"/>
               </TD>
               <TD>
                  <xsl:value-of select="V_1"/>
               </TD>
               <TD>
                  <xsl:value-of select="V_2"/>
               </TD>
               <TD>
                  <xsl:value-of select="Om_0"/>
               </TD>
               <TD>
                  <xsl:value-of select="Om_1"/>
               </TD>
               <TD>
                  <xsl:value-of select="Om_2"/>
               </TD>
               <TD>
                  <xsl:value-of select="Vc_0"/>
               </TD>
               <TD>
                  <xsl:value-of select="Vc_1"/>
               </TD>
               <TD>
                  <xsl:value-of select="Vc_2"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_00"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_01"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_02"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_10"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_11"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_12"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_20"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_21"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_22"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_30"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_31"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_32"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_40"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_41"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_42"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_50"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_51"/>
               </TD>
               <TD>
                  <xsl:value-of select="AmpR_52"/>
               </TD>
               <TD>
                  <xsl:value-of select="teta"/>
               </TD>
               <TD>
                  <xsl:value-of select="psy"/>
               </TD>
               <TD>
                  <xsl:value-of select="fi"/>
               </TD>
            </TR>
        </xsl:for-each>
    </TABLE>
  </xsl:template>
</xsl:stylesheet>

