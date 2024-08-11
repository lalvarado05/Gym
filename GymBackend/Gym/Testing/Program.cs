using CoreApp;
using DTOs;

namespace Testing;

internal class Program
{
    private static void Main(string[] args)
    {
        Double membershipCost = 20000;
        Double ptCost = 0;
        int percentage = 10;
        Double descuento = (1 - ((double)percentage / 100));
        Double AmountAfterDiscount = ptCost + membershipCost * descuento;

        Console.WriteLine("El total es:" + AmountAfterDiscount);
        Console.WriteLine("El descuento es:" + descuento);
        Console.WriteLine("El total es:" + AmountAfterDiscount);
        Console.WriteLine("El total es:" + AmountAfterDiscount);

        //NewestInvoice.Amount = ptCost + membershipCost;
        //NewestInvoice.AmountAfterDiscount = ptCost + membershipCost * (1 - (discount.Percentage / 100));
        //iCrud.Update(NewestInvoice);


    }
}