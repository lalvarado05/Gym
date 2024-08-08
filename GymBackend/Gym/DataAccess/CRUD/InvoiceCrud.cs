using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD
{
    public class InvoiceCrudFactory : CrudFactory
    {
        public InvoiceCrudFactory()
        {
            _sqlDao = SqlDao.GetInstance();
        }

        public override void Create(BaseDTO baseDto)
        {
            var invoice = baseDto as Invoice;

            var sqlOperation = new SqlOperation
            {
                ProcedureName = "CRE_INVOICE_PR"
            };

            sqlOperation.AddIntParam("P_UserId", invoice.UserId);
            sqlOperation.AddIntParam("P_DiscountId", invoice.DiscountId);
            sqlOperation.AddDoubleParam("P_Amount", invoice.Amount);
            sqlOperation.AddDoubleParam("P_AmountAfterDiscount", invoice.AmountAfterDiscount);
            sqlOperation.AddStringParam("P_PaymentMethod", invoice.PaymentMethod);
            sqlOperation.AddStringParam("P_IsConfirmed", invoice.IsConfirmed);
            sqlOperation.AddDateTimeParam("P_Created", invoice.Created);

            _sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDto)
        {
            var invoice = baseDto as Invoice;
            var sqlOperation = new SqlOperation
            {
                ProcedureName = "DEL_INVOICE_PR"
            };

            sqlOperation.AddIntParam("P_Id", invoice.Id);

            _sqlDao.ExecuteProcedure(sqlOperation);
        }

      

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstInvoices = new List<T>();
            var sqlOperation = new SqlOperation
            {
                ProcedureName = "RET_ALL_INVOICES_PR"
            };
            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var invoice = BuildInvoice(row);
                    lstInvoices.Add((T)Convert.ChangeType(invoice, typeof(T)));
                }
            }

            return lstInvoices;
        }

        public override T RetrieveById<T>(int id)
        {
            var sqlOperation = new SqlOperation
            {
                ProcedureName = "RET_INVOICE_BY_ID_PR"
            };
            sqlOperation.AddIntParam("P_Id", id);

            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
            if (lstResults.Count > 0)
            {
                var row = lstResults[0];
                var retInvoice = (T)Convert.ChangeType(BuildInvoice(row), typeof(T));
                return retInvoice;
            }

            return default;
        }

        public override void Update(BaseDTO baseDto)
        {
            var invoice = baseDto as Invoice;

            var sqlOperation = new SqlOperation
            {
                ProcedureName = "UPD_INVOICE_PR"
            };

            sqlOperation.AddIntParam("P_Id", invoice.Id); 
            sqlOperation.AddIntParam("P_UserId", invoice.UserId);
            sqlOperation.AddIntParam("P_DiscountId", invoice.DiscountId);
            sqlOperation.AddDoubleParam("P_Amount", invoice.Amount);
            sqlOperation.AddDoubleParam("P_AmountAfterDiscount", invoice.AmountAfterDiscount);
            sqlOperation.AddStringParam("P_PaymentMethod", invoice.PaymentMethod);
            sqlOperation.AddStringParam("P_IsConfirmed", invoice.IsConfirmed);
            

            _sqlDao.ExecuteProcedure(sqlOperation);
        }

        #region Funciones extras

        private Invoice BuildInvoice(Dictionary<string, object> row)
        {
            var invoiceToReturn = new Invoice
            {
                Id = (int)row["id"],
                UserId = (int)row["user_id"],
                DiscountId = (int)row["discount_id"],
                Amount = Convert.ToDouble(row["amount"]),
                AmountAfterDiscount = Convert.ToDouble(row["amount_after_discount"]),
                PaymentMethod = (string)row["payment_method"],
                IsConfirmed = (string)row["is_confirmed"],
                Created = (DateTime)row["created"]
            };
            return invoiceToReturn;
        }

        #endregion
    }
}