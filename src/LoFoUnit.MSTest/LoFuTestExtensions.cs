﻿using System.Threading.Tasks;
using LoFuUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoFoUnit.MSTest
{
    /// <summary>
    /// Extension methods for test fixtures.
    /// </summary>
    public static class LoFuTestExtensions
    {
        /// <summary>
        /// Runs the local functions in the containing test method derived from the <see cref="TestContext"/>.
        /// </summary>
        /// <param name="fixture">The test fixture.</param>
        /// <param name="testContext">The current <see cref="TestContext"/></param>
        /// <remarks>Derives the test method from <c>testContext.TestName</c></remarks>
        public static void Assert(this object fixture, TestContext testContext)
        {
            var method = fixture.GetType().GetMethod(testContext.TestName);

            new InternalLoFuTest().Assert(fixture, method);
        }

        /// <summary>
        /// Runs the local functions in the containing test method derived from the <see cref="TestContext"/>.
        /// </summary>
        /// <param name="fixture">The test fixture.</param>
        /// <param name="testContext">The current <see cref="TestContext"/></param>
        /// <remarks>Derives the test method from <c>testContext.TestName</c></remarks>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task AssertAsync(this object fixture, TestContext testContext)
        {
            var method = fixture.GetType().GetMethod(testContext.TestName);

            await new InternalLoFuTest().AssertAsync(fixture, method).ConfigureAwait(false);
        }
    }
}