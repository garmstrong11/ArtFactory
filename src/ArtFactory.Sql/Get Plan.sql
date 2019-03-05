SELECT TC.AccountID AS AccountId
     , TC.campaignID AS CampaignId
     , TD.documentID AS DocumentId
     , TP.planID AS PlanId
     , TP.planName AS PlanName
FROM XMPie.TBL_DOCUMENT TD
INNER JOIN XMPie.TBL_CAMPAIGN TC ON TD.campaignID = TC.campaignID
INNER JOIN XMPie.TBL_PLAN TP ON TC.campaignID = TP.campaignID
WHERE TD.isDeleted = 0
      AND TD.documentID = @DocumentId