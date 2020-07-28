---
title: GSTR2
keywords: GSTR2
sidebar: gstweb_sidebar
permalink: gst-nirvana-web/gstr2.html
folder: gstWeb
hide_sidebar: false
comments: false
---

# GSTR2

To file return for GSTR2, follow the given steps:

## Step-1: Connect with GSTN

**STEP-1**: Select the purchase invoice and right click on it. Select Open GSTN GSTR2 Sync. The sync page will display.

![](/images/connect-with-gstn-r2.png)


**STEP-2**: Click on Connect button. You will receive OTP on your registered mobile number.

**STEP-3**: Enter the OTP. Click on Submit. Success: Token Obtained message will show.

> Note: If your token is existing, when click on connect, it shows Existing token message.

![](/images/token-message-r2.png)


## Step-2: Generate Payload

**•**	To check the request payload of new and modified invoices, click on Generate Payload button in New/Modify section.

**•**	To check the request payload of deleted invoices, click on Generate Payload button in Delete section.

Generate Payload will be downloaded in the form of json file.

You can open json file with Notepad.

![](/images/generate-payload-r2.png)

>Notice: You can check the summary for particular GSTIN and Post period in Summary section.

## Step-3: Download Counter Party

To download counter party invoices(GSTR2A), click on Download button in Counter Party section.

When you download counter party invoices, success message will show.

You can view the counter party invoices by following the given steps:

**Step-1**: Click on Purchase menu and select Counter Party Invoices. The list of counter party invoices will appear.

![](/images/download-counter-party-r2.png)

**Step-2**: Select the invoice that you want to view and right click on it. Select View CP Invoice. The invoice detail will appear.

## Step-4: Accept/Reject

Match codes and action flags are determined based on matching of taxpayer and counterparty invoices.

|Match Code|Action Flag|
|:---------|:----------|
|OK|Accept(A)|
|TO|Upload (U)|
|CO|Reject (R) (If invoice available at counter party side)|
|Mismatch|Reject (R) |




You can mark accept or reject invoices as pending (P) invoice by following the given steps;

1.	Select the accept or reject invoice that you mark as pending. Right click on it.

2.	Select

a.	For Purchase Invoice – Edit Purchase Sale

b.	For Counter Party Invoice – View CP Invoice

3.	Click on Mark Pending button. Action flag(A/R) will change to Pending(P).

4.	If you want to unmark pending invoice, follow the step-1 or 2 again and click on Unmark Pending button.

>Note: Until unless counter party invoices are filed, you cannot accept or reject the invoices.

## Step-5: Save To GSTN

**•**	To save new/modified invoices to GSTN, click on Save to GSTN button in New/Modify section.

**•**	To save deleted invoices to GSTN, click on Save to GSTN button in Delete section.

After saving invoices, Success message will appear.

## Step-6: Download Summary

You can check the summary of invoices that you have saved to gstn for particular post period. To download the summary of saved invoices, click on Download button in Summary section.

Your downloaded summary will be display on sync page.

Make sure that summary and downloaded summary shown on sync page should be same.

## Step-7: Submit

Click on Submit button.

## Step-8: File

Click on File button.
