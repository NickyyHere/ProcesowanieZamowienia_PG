namespace ProcesowanieZamowienia_PG
{
    internal enum OrderStates
    {
        NEW,
        STORAGE,
        SENT,
        RETURNED,
        ERROR,
        CLOSED
    }
}
