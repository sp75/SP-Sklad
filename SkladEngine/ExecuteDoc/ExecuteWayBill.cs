using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkladEngine.ExecuteDoc
{
   public class ExecuteWayBill
    {
       private int _doc_type { get; set; }

       ExecuteWayBill(int doc_type)
       {
           _doc_type = doc_type;
       }

       public void ExecuteReturnIn(int wbill_id)
       {

       }

       public void StornoReturnIn(int wbill_id)
       {


      /*     declare id_cursor  cursor local FAST_FORWARD for 
		select wbd.PosId, wbd.matid, wbd.wid, wbd.amount, wbd.currid, wbd.num, wbd.onvalue ,  p_wbd.ondate, p_wbd.price, rtr.outposid, rtr.pposid , p_wbd.BasePrice 
        from waybilldet wbd
        inner join returnrel rtr on rtr.posid =  wbd.PosId
        inner join waybilldet p_wbd on p_wbd.posid = rtr.pposid   --Взнаємо за якою ціною було зроблена закупка (приход)
        where wbd.wbillid = @WBILLID

        open    id_cursor
 
        fetch next from id_cursor into @POSID , @matid, @wid, @amount, @currid, @num, @onvalue ,  @wbd_ondate, @price, @V_OUTPOSID, @V_PPOSID, @BasePrice  
        while (@@fetch_status = 0)
        begin
                 -- Добавляем в waybilldet позицію (прибуток)
                 insert into waybilldet (WBILLID,  matid, wid, amount, currid, ondate, num, onvalue,price, BasePrice )
                                 values (@WBILLID, @matid, @wid, @amount, @currid, @ondate,@num, @onvalue, @price , @BasePrice) ;

                  SET @NEW_POSID = SCOPE_IDENTITY();  
                 --Оновлюемо поле dposid яка вказує на нову позицію прихода в waybilldet
                 update returnrel set dposid =@NEW_POSID where posid =@POSID and outposid=@V_OUTPOSID and pposid=@V_PPOSID ;

                 -- Добавляем в WMATTURN обороти по матеріалу згідно нового waybilldet
                 insert into WMATTURN  (POSID, WID, MATID, ondate, TURNTYPE, AMOUNT, SOURCEID)
                               values (@NEW_POSID, @wid, @matid, @ondate, 1,@amount, @NEW_POSID);
         
          fetch next from id_cursor into @POSID , @matid, @wid, @amount, @currid, @num, @onvalue ,  @wbd_ondate, @price, @V_OUTPOSID, @V_PPOSID, @BasePrice   
        end

        close      id_cursor
        deallocate id_cursor

        update waybilllist set checked = 1 where wbillid =@WBILLID ;
            */
       }
    }
}
