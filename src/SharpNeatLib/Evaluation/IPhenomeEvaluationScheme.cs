﻿using System.Collections.Generic;

namespace SharpNeat.Evaluation
{
    /// <summary>
    /// Represents an overall phenome evaluation scheme.
    /// </summary>
    /// <remarks>
    /// Provides information related to the evaluation scheme, and a method for creating new evaluator instances.
    /// </remarks>
    /// <typeparam name="TPhenome">The phenome type to be evaluated.</typeparam>
    public interface IPhenomeEvaluationScheme<TPhenome>
    {
        #region Properties

        /// <summary>
        /// Indicates if the evaluation scheme is deterministic, i.e. will always return the same fitness score for a given genome.
        /// </summary>
        /// <remarks>
        /// An evaluation scheme that has some random/stochastic characteristics may give a different fitness score at each invocation 
        /// for the same genome, such as scheme is non-deterministic.
        /// </remarks>
        bool IsDeterministic { get; }

        /// <summary>
        /// Gets a fitness comparer for the scheme.
        /// </summary>
        /// <remarks>
        /// Typically there is a single fitness score whereby a higher score is better, however if there are multiple fitness scores
        /// per genome then we need a more general purpose comparer to determine an ordering on FitnessInfo(s), i.e. to be able to 
        /// determine which is the better FitnessInfo between any two.
        /// </remarks>
        IComparer<FitnessInfo> FitnessComparer { get; }

        /// <summary>
        /// Gets a null fitness score for the scheme, i.e. for genomes that cannot be assigned a fitness score for whatever reason,
        /// e.g. if a genome failed to decode to a viable phenome that could be tested.
        /// </summary>
        FitnessInfo NullFitness { get; }

        /// <summary>
        /// Indicates if the evaluators created by <see cref="Create"/> have state.
        /// </summary>
        /// <remarks>
        /// If an evaluator has no state then it is sufficient to create a single instance and to use that evaluator concurrently on multiple threads.
        /// If an evaluator has state then concurrent use requires the creation of one evaluator instance per thread.
        /// </remarks>
        bool EvaluatorsHaveState { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new evaluator object.
        /// </summary>
        /// <returns>A new instance of <see cref="IPhenomeEvaluator{T}"/>.</returns>
        IPhenomeEvaluator<TPhenome> Create();

        /// <summary>
        /// Accepts a <see cref="FitnessInfo"/>, which is intended to be from the fittest genome in the population, and returns a boolean
        /// that indicates if the evolution algorithm can stop, i.e. because the fitness is the best that can be achieved (or good enough).
        /// </summary>
        /// <param name="fitnessInfo">The fitness info object to test.</param>
        /// <returns>Returns true if the fitness is good enough to signal the evolution algorithm to stop.</returns>
        bool TestForStopCondition(FitnessInfo fitnessInfo);

        #endregion
    }
}