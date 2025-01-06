// JavaScript Document

function displayAnnouncement()
{		
	var iframeContent 		= document.getElementById("content_ifrm");
	var announcementContent = document.getElementById("announcementContent");
	var loading 			= document.getElementById("announcementLoading");
	
	if(iframeContent && announcementContent && loading)
	{

		announcementContent.style.display = "none";
		announcementContent.style.visibility = "visible";
		iframeContent.style.display = "block";
		iframeContent.style.visibility = "visible";
	}
}