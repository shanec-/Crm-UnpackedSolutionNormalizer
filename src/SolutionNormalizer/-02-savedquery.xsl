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
      <xsl:apply-templates select="savedquery">
        <xsl:sort select="LocalizedNames/LocalizedName/@description"/>
      </xsl:apply-templates>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="FormXml">
    <xsl:copy>
      <xsl:apply-templates select="forms">
        <xsl:sort select="@type"/>
        <xsl:sort select="formid"/>
      </xsl:apply-templates>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="forms">
    <xsl:copy>
      <xsl:apply-templates select="systemform">
        <xsl:sort select="formid"/>
      </xsl:apply-templates>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="Workflows">
    <xsl:copy>
      <xsl:apply-templates select="Workflow">
        <xsl:sort select="@WorkflowId"/>
      </xsl:apply-templates>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="WebResources">
    <xsl:copy>
      <xsl:apply-templates select="WebResource">
        <xsl:sort select="WebResourceId"/>
      </xsl:apply-templates>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="SdkMessageProcessingSteps">
    <xsl:copy>
      <xsl:apply-templates select="SdkMessageProcessingStep">
        <xsl:sort select="@Name"/>
      </xsl:apply-templates>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="Dashboards">
    <xsl:copy>
      <xsl:apply-templates select="Dashboard">
        <xsl:sort select="FormId"/>
      </xsl:apply-templates>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="MissingDependencies">
    <xsl:copy>
      <xsl:apply-templates select="MissingDependency">
        <xsl:sort select="Required/@id"/>
        <xsl:sort select="Required/@key"/>
        <xsl:sort select="Required/@schemaName"/>
        <xsl:sort select="Required/@displayName"/>
        <xsl:sort select="Required/@solution"/>
      </xsl:apply-templates>
    </xsl:copy>
  </xsl:template>
</xsl:stylesheet>
