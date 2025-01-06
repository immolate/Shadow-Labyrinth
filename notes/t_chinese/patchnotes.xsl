<?xml version="1.0" encoding="iso-8859-1"?>

<!DOCTYPE xsl:stylesheet  [
	<!ENTITY nbsp   "&#160;">
	<!ENTITY copy   "&#169;">
	<!ENTITY reg    "&#174;">
	<!ENTITY trade  "&#8482;">
	<!ENTITY mdash  "&#8212;">
	<!ENTITY ldquo  "&#8220;">
	<!ENTITY rdquo  "&#8221;">
	<!ENTITY pound  "&#163;">
	<!ENTITY yen    "&#165;">
	<!ENTITY euro   "&#8364;">
]>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<!-- Base Template -->
	<xsl:template match="/">
		<html>

		<!-- CSS Stylesheet -->
		<link href="patcher.css" rel="stylesheet" type="text/css" />

		<body>
			<div id="patchnotes">
				<xsl:apply-templates />
			</div>
		</body>

		</html>
	</xsl:template>

	<xsl:template match="*">
		<xsl:choose>
			<xsl:when test="name(.) = 'patchnotes'">
				<xsl:call-template name="patchnotes" />
			</xsl:when>
			<xsl:when test="name(.) = 'patchnote'">
				<xsl:call-template name="patchnote" />
			</xsl:when>
			<xsl:when test="name(.) = 'category'">
				<xsl:call-template name="category" />
			</xsl:when>
			<xsl:when test="name(.) = 'section'">
				<xsl:call-template name="section" />
			</xsl:when>
			<xsl:when test="name(.) = 'subsection'">
				<xsl:call-template name="subsection" />
			</xsl:when>
			<xsl:when test="name(.) = 'career'">
				<xsl:call-template name="career" />
			</xsl:when>
			<xsl:when test="name(.) = 'item'">
				<xsl:call-template name="item" />
			</xsl:when>
			<xsl:when test="name(.) = 'subitem'">
				<xsl:call-template name="subitem" />
			</xsl:when>
			<xsl:when test="name(.) = 'comment'">
				<xsl:call-template name="comment" />
			</xsl:when>
			<xsl:when test="name(.) = 'description'">
				<xsl:call-template name="description" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:copy-of select="." />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<!-- Patchnotes tag template -->
	<xsl:template name="patchnotes">
		<div class="header">
			<xsl:value-of select="@title" />
			<div class="gameName">Ultima Online: SA Client</div>
		</div>

		<div id="container">
			<xsl:apply-templates />
		</div>
	</xsl:template>

	<!-- Patchnote tag template -->
	<xsl:template name="patchnote">
		<div class="category"><xsl:value-of select="@title" />: <xsl:value-of select="@date" /></div>
		<xsl:apply-templates />
	</xsl:template>

	<!-- Category tag template -->
	<xsl:template name="category">
		<div class="category"><xsl:value-of select="@name" /></div>
		<xsl:apply-templates />
	</xsl:template>

	<!-- Section tag template -->
	<xsl:template name="section">
		<div class="section"><xsl:value-of select="@name" /></div>
		<xsl:apply-templates />
	</xsl:template>

	<!-- Subsection tag template -->
	<xsl:template name="subsection">
		<div class="subsection"><xsl:value-of select="@name" />:</div>
		<xsl:apply-templates />
	</xsl:template>

	<!-- Career tag template -->
	<xsl:template name="career">
		<div class="career"><xsl:value-of select="@name" /></div>
		<xsl:apply-templates />
	</xsl:template>

	<!-- Description template -->
	<xsl:template name="description">
		<div class="description">
			<xsl:apply-templates />
		</div>
	</xsl:template>

	<!-- Comment tag template -->
	<xsl:template name="comment">
		<div class="comment">
			<xsl:apply-templates />
		</div>
	</xsl:template>

	<!-- Item tag template -->
	<xsl:template name="item">
		<div class="item">
			* <xsl:apply-templates />
		</div>
	</xsl:template>

	<!-- Subitem tag template -->
	<xsl:template name="subitem">
		<div class="subitem">
			- <xsl:apply-templates />
		</div>
	</xsl:template>

</xsl:stylesheet>