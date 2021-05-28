namespace HarwexBank
{
    public interface ICurrencyRatesObserver
    {
        // Получает обновление от издателя
        void NewRates(ICurrencyRatesSubject subject);
    }
    
    public interface ICurrencyRatesSubject
    {
        // Присоединяет наблюдателя к издателю.
        void Attach(ICurrencyRatesObserver observer);

        // Отсоединяет наблюдателя от издателя.
        void Detach(ICurrencyRatesObserver observer);

        // Уведомляет всех наблюдателей о событии.
        void Notify();
    }
}