# TSP-greedy
Metaheuristics
Ulepszenie przeszukiwania lokalnego typu greedy umożliwiające "wyskoczenie" z lokalnego optimum.
Jeśli sąsiad jest lepszy od aktualnego rozwiązania, wybieramy go jako aktualne
rozwiązanie (tak samo jak w greedy)
eśli sąsiad jest gorszy od aktualnego rozwiązania przyjmujemy go jako aktualne z
pewnym prawdopodobieństwem

p = e^(Delta/T)

Delta - różnica między rozwiązaniem aktualnym a sąsiadem, wartość ujemna (dla dodatniej sąsiad jest lepszy od r. akt.)
T - temperatura - parametr regulujący prawdopodobieństwo. Z kolejnymi iteracjami powinien być coraz mniejszy. Dla T dążącego do 0 algorytm przekształca się w local search’a. Parametr T powinien być wybrany z uwzględnieniem
wartości funkcji celu występujących w danej instancji.
