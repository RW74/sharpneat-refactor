﻿
namespace SharpNeat.EvolutionAlgorithm
{
    public interface IEvolutionAlgorithm
    {
        /// <summary>
        /// Gets evolutionary algorithm statistics.
        /// </summary>
        EvolutionAlgorithmStatistics Stats { get; }

        /// <summary>
        /// Initialise the evolutionary algorithm.
        /// </summary>
        void Initialise();

        /// <summary>
        /// Perform one generation of the evolutionary algorithm.
        /// </summary>
        void PerformOneGeneration();
    }
}
