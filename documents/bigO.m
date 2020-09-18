clear all
references_n = [1,10,100,1000,10000,100000,1000000,10000000];
reference_notation = @(n) n.*log(n);
references_times = reference_notation(references_n);

figure(1)
plot(references_n, references_times);
xlabel("Problem Size");
ylabel("Complexity");
title("Merge Sort Time Complexity -- Expected");

figure(2)
time = [0.1685, 0.3561,0.6392, 1.9895, 20.0446, 289.518, 3365.4725, 50038.4494];
plot(references_n, time);
xlabel("Problem Size");
ylabel("Time (ms)");
title("Merge Sort Time Complexity -- Implemented");