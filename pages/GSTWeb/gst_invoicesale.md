---
title: INVOICE SALE
keywords: INVOICE SALE
sidebar: gstweb_sidebar
permalink: gst-nirvana-web/invoice-sale.html
folder: gstWeb
hide_sidebar: false
comments: false
---

# INVOICE SALE

## Create

You can create your sale invoice by following the given steps:

**STEP-1**: Click on New menu and select Invoice Sale. The Invoice Sale form will appear.

![](/images/invoice-sale-create.png)

**STEP-2**: Select name of customer in Customer field.

**STEP-3**: Enter invoice number in Invoice No. field.

**STEP-4**: Select invoice type in Invoice Type field. If any invoice type has sub type, Invoice Sub Type field will appear. Suppose you select B2B invoice type, you will see B2B invoice type field.

**STEP-5**: Select Campus.

**STEP-6**: Select Supply Type.

**STEP-7**: Select Place of Supply.

**STEP-8**: If your invoice attracting reverse charge, select Yes in Reverse Charge field otherwise select No.

**STEP-9**: Select Division.

**STEP-10**: If you want to sale your products through e-commerce operator, you have to enter GSTIN of your e-commerce operator in ETIN field.

**STEP-11**: Click on **Add Item** button to add item in your invoice. For add more than one item you have to click on Add Item again. The new item will add in your invoice.

**STEP-12**: Fill the details of item/items.

**STEP-13**: Click on **Save** button. If your invoice has
errors, it will give indication with red mark otherwise saving progress bar will show and Success message will display.

![](/images/invoice-sale-create-detail.png)


## List

Click on **Sale menu and select Invoices**. Invoices list will display.

![](/images/invoice-sale-list.png)

## Edit

You can edit the sale invoice by following the given steps:

**STEP-1**: **Select** the invoice that you want to edit. **Right click** on it. Select Edit Invoice Sale option. The Invoice Sale form will appear.

![](/images/invoice-sale-edit.png)

**STEP-2**: Edit the invoice.

**STEP-3**: Click on **Save** button.

## Delete

You can delete the sale invoice by following the given steps:

**STEP-1**: **Select** the invoice that you want to delete. **Right click** on it. Select **Edit Invoice Sale** option. The Invoice Sale form will appear.

**STEP-2**: Click on **Delete** button. After progress bar, dialog box will appear. Click on **Save**.

![](/images/invoice-sale-delete.png)

### Supply Type

When the state of campus and place of supply are same, supply type should be Intra type. Otherwise supply type should be Inter type.

### Reverse Charge

If you select reverse charge as No, then your Total Invoice Value will be the sum of tax value and taxable value. If you select reverse charge as Yes, your Total Invoice Value is same as taxable value. In this case, tax value will not be included in total invoice value.

### TaxPaid

If you receive advance payment from your vendor, you have to enter the total advance amount that you have received from your vendor in TaxPaid tab according to tax rate.

![](/images/invoice-sale-tax-paid.png)

### ETIN

If you want to sale your product through e-commerce operator, you have to enter your e-commerce        operator gstin number in ETIIN field.

### Zero Tax Item

If your item is considered as zero tax item (tax rate = 0)
then you have to select one of the three zero tax types:
1.	Nil Rated
2.	Exempted
3.	Non-gst


### Zero Rated Invoice

B2B -> SEZWOP (SEZ without payment) and EXP -> WPOY (Export without payment) invoices are known as zero rated invoices. Items of these invoices are zero tax.


![](/images/invoice-sale-zero-rated.png)
