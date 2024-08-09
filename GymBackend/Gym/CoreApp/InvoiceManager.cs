using DataAccess.CRUD;
using DTOs;

namespace CoreApp
{
    public class InvoiceManager
    {
        public void Create(Invoice invoice)
        {
            var iCrud = new InvoiceCrudFactory();

            // Validar la factura antes de crearla
            ValidateInvoice(invoice);

            iCrud.Create(invoice);
        }

        public void Update(Invoice invoice)
        {
            var iCrud = new InvoiceCrudFactory();
            iCrud.Update(invoice);
        }

        public void Delete(Invoice invoice)
        {
            var iCrud = new InvoiceCrudFactory();
            iCrud.Delete(invoice);
        }

        public List<Invoice> RetrieveAll()
        {
            var iCrud = new InvoiceCrudFactory();
            return iCrud.RetrieveAll<Invoice>();
        }

        public Invoice RetrieveById(int id)
        {
            var iCrud = new InvoiceCrudFactory();
            return iCrud.RetrieveById<Invoice>(id);
        }

        #region Validations

        private void ValidateInvoice(Invoice invoice)
        {
            if (invoice.MembershipID == 0)
            {
                throw new Exception("Por favor selecciona un tipo de membresía.");
            }
            if (invoice.UserId <= 0)
            {
                throw new Exception("Por favor selecciona un usuario.");
            }
            if (invoice.PaymentMethod == "empty")
            {
                throw new Exception("Por favor selecciona un método de pago.");
            }
            


        }

        #endregion
    }
}