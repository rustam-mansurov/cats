<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  version="1.0">
  <xsl:template match="/">


<H1 ALIGN="CENTER">Моделирование траектории движения снаряда</H1>
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
	<xsl:for-each select="program/resultdata/shot">
	<H3 ALIGN="LEFT">Выстрел <xsl:value-of select="@number"/>,  снаряд <xsl:value-of select="snaryad"/> </H3>
      <TABLE WIDTH="1200" BORDER="1" CELLPADDING="1" cellspacing="0">
         <THEAD>
            <TH>t, с</TH>
            <TH>x, м</TH>
            <TH>y, м </TH>
            <TH>z, м </TH>
            <TH>Vk, м/с </TH>
            <TH>teta, град </TH>
            <TH>psy, град </TH>
            <TH>omega_x, рад/с </TH>    
            <TH>pi, __ </TH>
            <TH>m, кг </TH>
            <TH>omega1, рад/с </TH>
            <TH>omega2, рад/с </TH>
            <TH>delta1, град </TH>
            <TH>delta2, град </TH>      
         </THEAD>
         <xsl:for-each select="values/record">
            <TR ALIGN="CENTER">
               <TD>
                  <xsl:value-of select="t"/>
               </TD>
               <TD>
                  <xsl:value-of select="x"/>
               </TD>
               <TD>
                  <xsl:value-of select="y"/>
               </TD>
               <TD>
                  <xsl:value-of select="z"/>
               </TD>
               <TD>
                  <xsl:value-of select="Vk"/>
               </TD>
               <TD>
                  <xsl:value-of select="teta"/>
               </TD>
               <TD>
                  <xsl:value-of select="psy"/>
               </TD>
               <TD>
                  <xsl:value-of select="omega_x"/>
               </TD>
               <TD>
                  <xsl:value-of select="pi"/>
               </TD>
               <TD>
                  <xsl:value-of select="m"/>
               </TD>
               <TD>
                  <xsl:value-of select="omega1"/>
               </TD>
               <TD>
                  <xsl:value-of select="omega2"/>
               </TD>
               <TD>
                  <xsl:value-of select="delta1"/>
               </TD>
               <TD>
                  <xsl:value-of select="delta2"/>
               </TD>
            </TR>
        </xsl:for-each>		
    </TABLE>
	</xsl:for-each>
  </xsl:template>
</xsl:stylesheet>

