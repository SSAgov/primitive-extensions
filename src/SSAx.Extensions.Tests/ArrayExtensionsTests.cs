using System.Linq;
using Xunit;

namespace SSAx.Extensions.Tests
{
    public class ArrayExtensionsTests
    {
        [Fact]
        public void CreateSubsets_Given_0_nodes_expect_0_subsets()
        {
            string[] words = { };

            var subsets = words.CreateSubsets();
            Assert.Empty(subsets);
        }

        [Fact]
        public void CreateSubsets_Given_1_nodes_expect_1_subsets()
        {
            string s = "Hello";
            string[] words = s.Split(' ');

            var subsets = words.CreateSubsets();
            Assert.Single(subsets);

            Assert.Contains(new string[] { "Hello" }, subsets);
        }

        [Fact]
        public void CreateSubsets_Given_2_nodes_expect_3_subsets()
        {
            string s = "Hello there,";
            string[] words = s.Split(' ');

            var subsets = words.CreateSubsets();
            Assert.Equal(3,subsets.Count());
            Assert.Contains(new string[] { "Hello" }, subsets);
            Assert.Contains(new string[] { "there," }, subsets);
            Assert.Contains(new string[] { "Hello","there," }, subsets);

        }

        [Fact]
        public void CreateSubsets_Given_3_nodes_expect_7_subsets()
        {
            string s = "Hello there, how";
            string[] words = s.Split(' ');

            var subsets = words.CreateSubsets();
            Assert.Equal(7, subsets.Count());
            Assert.Contains(new string[] { "Hello" }, subsets);
            Assert.Contains(new string[] { "there," }, subsets);
            Assert.Contains(new string[] { "how" }, subsets);
            Assert.Contains(new string[] { "Hello", "there," }, subsets);
            Assert.Contains(new string[] { "there,", "how"}, subsets);
            Assert.Contains(new string[] { "Hello", "how" }, subsets);
            Assert.Contains(new string[] { "Hello", "there,", "how" }, subsets);
        }

        [Fact]
        public void CreateSubsets_Given_4_nodes_expect_15_subsets()
        {
            string s = "Hello there, how are";
            string[] words = s.Split(' ');

            var subsets = words.CreateSubsets();
            Assert.Equal(15, subsets.Count());
            Assert.Contains(new string[] { "Hello" }, subsets); // 1
            Assert.Contains(new string[] { "there," }, subsets);  // 2
            Assert.Contains(new string[] { "how" }, subsets); // 3
            Assert.Contains(new string[] { "are" }, subsets); // 4
            Assert.Contains(new string[] { "Hello", "there," }, subsets); // 5
            Assert.Contains(new string[] { "Hello", "how" }, subsets); // 6
            Assert.Contains(new string[] { "Hello", "are" }, subsets); // 7
            Assert.Contains(new string[] { "there,", "how" }, subsets); // 8
            Assert.Contains(new string[] { "there,", "are" }, subsets); // 9
            Assert.Contains(new string[] { "how,", "are" }, subsets); // 10
            Assert.Contains(new string[] { "Hello", "there,", "how" }, subsets); // 11
            Assert.Contains(new string[] { "Hello", "there,", "are" }, subsets); // 12
            Assert.Contains(new string[] { "Hello", "how", "are" }, subsets); // 13
            Assert.Contains(new string[] { "there,", "how", "are" }, subsets); // 14
            Assert.Contains(new string[] { "Hello", "there,", "how", "are" }, subsets); //15

        }

        [Fact]
        public void CreateSubsets_Given_5_nodes_expect_31_subsets()
        {
            string s = "Hello there, how are you?";
            string[] words = s.Split(' ');

            var subsets = words.CreateSubsets();
            Assert.Equal(31, subsets.Count);
        }


    }
}

