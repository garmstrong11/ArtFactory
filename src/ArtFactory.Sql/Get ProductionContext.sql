SELECT *
FROM ov.ProductionContext PC
WHERE PC.AccountId = @AccountId
AND PC.CampaignId = @CampaignId
And PC.DocumentId = @DocumentId