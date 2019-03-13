SELECT
       TA.AccountID AS AccountId,
       TC.campaignID AS CampaignId,
       TD.documentID AS DocumentId,
       TP.planID AS PlanId,
       TA.accountName AS AccountName,
       TC.campaignName AS CampaignName,
       TD.documentName AS DocumentName,
       TP.planName AS PlanName,
       TD.documentTypeParams.value('(/DOCUMENT_TYPE/PAGE_WIDTH)[1]', 'decimal') AS PageWidth,
       TD.documentTypeParams.value('(/DOCUMENT_TYPE/PAGE_HEIGHT)[1]', 'decimal') AS PageHeight,
       TD.documentTypeParams.value('(/DOCUMENT_TYPE/PAGE_NUM)[1]', 'int') AS PageCount,
       PC.PrintSettingsJobId,
       PC.ProofSettingsJobId
FROM XMPIE.TBL_DOCUMENT TD
INNER JOIN XMPie.TBL_CAMPAIGN TC ON TD.campaignID = TC.campaignID
INNER JOIN XMPie.TBL_ACCOUNT TA ON tc.AccountID = TA.accountID
INNER JOIN XMPie.TBL_PLAN TP ON TC.campaignID = TP.campaignID
INNER JOIN ov.ProductionContext PC
  ON TD.documentID = PC.DocumentId
       AND TA.accountID = PC.AccountId
       AND TC.campaignID = PC.CampaignId
WHERE TD.isDeleted = 0
      AND TC.isDeleted = 0
      AND TA.isDeleted = 0
      AND TP.isDeleted = 0
      AND TD.documentID = @DocumentId -- 183