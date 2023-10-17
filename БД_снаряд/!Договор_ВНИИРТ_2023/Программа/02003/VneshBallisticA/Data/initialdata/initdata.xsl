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
            </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>

