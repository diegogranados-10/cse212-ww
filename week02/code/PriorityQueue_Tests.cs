using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities and dequeue them. Higher priority items should be dequeued first.
    // Expected Result: Items are dequeued in priority order: "High" (priority 10), "Medium" (priority 5), "Low" (priority 1)
    // Defect(s) Found: The Dequeue method had three bugs: (1) The loop condition was "index < _queue.Count - 1" which skipped the last item,
    // (2) The comparison used ">=" instead of ">" which violated FIFO for equal priorities, and (3) The item was not removed from the queue after being dequeued.
    public void TestPriorityQueue_BasicPriorityOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 10);
        priorityQueue.Enqueue("Medium", 5);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the same highest priority. The first one added (FIFO) should be dequeued first.
    // Expected Result: When multiple items have the same highest priority, they are dequeued in FIFO order: "First", "Second", "Third"
    // Defect(s) Found: The Dequeue method used ">=" comparison instead of ">", causing it to select the last item with equal priority instead of the first (FIFO violation).
    // Also, items were not removed from the queue after being dequeued.
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Third", 5);
        priorityQueue.Enqueue("Low", 1);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items with mixed priorities, including multiple items with the same highest priority.
    // Expected Result: Highest priority items are dequeued first, and among items with the same priority, FIFO order is maintained.
    // Defect(s) Found: The Dequeue method used ">=" comparison instead of ">", causing it to select the last item with equal priority instead of the first (FIFO violation).
    // Also, items were not removed from the queue after being dequeued.
    public void TestPriorityQueue_MixedPrioritiesWithTies()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 3);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 5);
        priorityQueue.Enqueue("D", 2);
        priorityQueue.Enqueue("E", 5);

        // B, C, and E all have priority 5, so they should be dequeued in order: B, C, E
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("E", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: InvalidOperationException should be thrown with message "The queue is empty."
    // Defect(s) Found: No defects found. The empty queue exception handling is implemented correctly.
    public void TestPriorityQueue_EmptyQueueException()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Enqueue a single item and dequeue it, then try to dequeue again from empty queue.
    // Expected Result: First dequeue returns the item, second dequeue throws InvalidOperationException.
    // Defect(s) Found: The Dequeue method did not remove items from the queue after returning their value, causing the queue to never become empty.
    public void TestPriorityQueue_SingleItemThenEmpty()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Only", 10);

        Assert.AreEqual("Only", priorityQueue.Dequeue());

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Enqueue items where the last item has the highest priority to test if the loop checks all items.
    // Expected Result: The last item with highest priority should be dequeued first.
    // Defect(s) Found: The loop condition was "index < _queue.Count - 1" which skipped checking the last item in the queue, causing it to miss the highest priority item when it was at the end.
    public void TestPriorityQueue_LastItemHighestPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Second", 2);
        priorityQueue.Enqueue("Third", 3);
        priorityQueue.Enqueue("Highest", 10);

        Assert.AreEqual("Highest", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("First", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items with negative priorities and zero priority to test edge cases.
    // Expected Result: Items are dequeued in priority order, with higher numbers having higher priority.
    // Defect(s) Found: The loop condition was "index < _queue.Count - 1" which skipped checking the last item in the queue, causing incorrect behavior when the highest priority item was at the end.
    public void TestPriorityQueue_NegativeAndZeroPriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Negative", -5);
        priorityQueue.Enqueue("Zero", 0);
        priorityQueue.Enqueue("Positive", 5);

        Assert.AreEqual("Positive", priorityQueue.Dequeue());
        Assert.AreEqual("Zero", priorityQueue.Dequeue());
        Assert.AreEqual("Negative", priorityQueue.Dequeue());
    }
}