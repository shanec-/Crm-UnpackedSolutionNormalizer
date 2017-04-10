<xsl:stylesheet version="1.0"
 xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output omit-xml-declaration="yes" indent="yes"/>
  <xsl:strip-space elements="*"/>

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="SavedQueries/savedqueries">
    <xsl:copy>
      <!--<xsl:apply-templates select="@*"/>
      <xsl:apply-templates select="*"/>-->
      <xsl:apply-templates select="savedquery">
        <xsl:sort select="LocalizedNames/LocalizedName/@description"/>
      </xsl:apply-templates>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="FormXml">
    <xsl:copy>
      <!--<xsl:apply-templates select="@*"/>
      <xsl:apply-templates select="*"/>-->
      <xsl:apply-templates select="forms">
        <xsl:sort select="@type"/>
      </xsl:apply-templates>
    </xsl:copy>
  </xsl:template>
</xsl:stylesheet>
