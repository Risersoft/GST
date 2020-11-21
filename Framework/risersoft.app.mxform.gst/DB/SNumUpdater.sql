	
	declare @invoiceid int
	declare @invoiceitemid int
	declare @linesnum int
	declare @lastwoid int
	

		declare Curs1 cursor fast_forward
		for

		SELECT top 5000 invoiceitemgstid,invoiceid from invoiceitemgst order by invoiceid desc,linesnum
	

	
		open curs1		
	
		set @lastwoid =0
		set @linesnum=0
		while (0=0) begin
			
			fetch next from Curs1 into @invoiceitemid, @invoiceid
			
	
			if (@@fetch_status<>0) break
			
			if @lastwoid<>@invoiceid
			begin
				set @linesnum=1
			end
			else
			begin
				set @linesnum=@linesnum+1
			end
			set @lastwoid = @invoiceid
			print @invoiceid
			print @invoiceitemid
			update invoiceitemgst set linesnum=@linesnum where invoiceitemgstid = @invoiceitemid
		end
Close curs1
deallocate curs1




