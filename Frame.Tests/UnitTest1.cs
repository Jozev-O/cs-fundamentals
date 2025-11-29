namespace Frame.Tests
{
    public class FrameTests
    {
        [Fact]
        public void Constructor_With_Name_Creates_Frame_With_Empty_Slots()
        {
            // Arrange & Act
            var frame = new Frame("TestFrame");

            // Assert
            Assert.Equal("TestFrame", frame.GetName());
            Assert.Null(frame.GetParent());
            Assert.Empty(frame.GetSlots());
        }

        [Fact]
        public void Constructor_With_Parent_Sets_Parent_Correctly()
        {
            // Arrange
            var parentFrame = new Frame("ParentFrame");

            // Act
            var childFrame = new Frame("ChildFrame", parentFrame);

            // Assert
            Assert.Equal("ChildFrame", childFrame.GetName());
            Assert.Equal(parentFrame, childFrame.GetParent());
        }

        [Fact]
        public void Constructor_With_Slots_Initializes_Slots_Correctly()
        {
            // Arrange
            var initialSlots = new Dictionary<string, object>
            {
                ["slot1"] = "value1",
                ["slot2"] = 42
            };

            // Act
            var frame = new Frame("TestFrame", null, initialSlots);

            // Assert
            var slots = frame.GetSlots();
            Assert.Equal(2, slots.Count);
            Assert.Equal("value1", slots["slot1"]);
            Assert.Equal(42, slots["slot2"]);
        }

        [Fact]
        public void GetValue_Returns_Value_From_Current_Frame()
        {
            // Arrange
            var frame = new Frame("TestFrame");
            frame.AddSlot("testSlot", "testValue");

            // Act
            var value = frame.GetValue("testSlot");

            // Assert
            Assert.Equal("testValue", value);
        }

        [Fact]
        public void GetValue_Returns_Value_From_Parent_Frame()
        {
            // Arrange
            var parentFrame = new Frame("ParentFrame");
            parentFrame.AddSlot("inheritedSlot", "parentValue");

            var childFrame = new Frame("ChildFrame", parentFrame);

            // Act
            var value = childFrame.GetValue("inheritedSlot");

            // Assert
            Assert.Equal("parentValue", value);
        }

        [Fact]
        public void GetValue_Throws_KeyNotFoundException_When_Slot_Not_Found()
        {
            // Arrange
            var frame = new Frame("TestFrame");

            // Act & Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => frame.GetValue("nonExistentSlot"));
            Assert.Contains("nonExistentSlot", exception.Message);
            Assert.Contains("TestFrame", exception.Message);
        }

        [Fact]
        public void GetValue_Throws_ArgumentNullException_For_Empty_Slot_Name()
        {
            // Arrange
            var frame = new Frame("TestFrame");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => frame.GetValue(""));
            Assert.Throws<ArgumentNullException>(() => frame.GetValue(null));
        }

        [Fact]
        public void GetValue_Inheritance_Chain_Works_Correctly()
        {
            // Arrange
            var grandParent = new Frame("GrandParent");
            grandParent.AddSlot("grandParentSlot", "grandParentValue");

            var parent = new Frame("Parent", grandParent);
            parent.AddSlot("parentSlot", "parentValue");

            var child = new Frame("Child", parent);
            child.AddSlot("childSlot", "childValue");

            // Act & Assert
            Assert.Equal("childValue", child.GetValue("childSlot"));
            Assert.Equal("parentValue", child.GetValue("parentSlot"));
            Assert.Equal("grandParentValue", child.GetValue("grandParentSlot"));
        }

        [Fact]
        public void SetValue_Updates_Existing_Slot()
        {
            // Arrange
            var frame = new Frame("TestFrame");
            frame.AddSlot("testSlot", "initialValue");

            // Act
            frame.SetValue("testSlot", "updatedValue");

            // Assert
            Assert.Equal("updatedValue", frame.GetValue("testSlot"));
        }

        [Fact]
        public void SetValue_Creates_New_Slot_If_Not_Exists()
        {
            // Arrange
            var frame = new Frame("TestFrame");

            // Act
            frame.SetValue("newSlot", "newValue");

            // Assert
            Assert.Equal("newValue", frame.GetValue("newSlot"));
        }

        [Fact]
        public void SetValue_Throws_ArgumentNullException_For_Empty_Slot_Name()
        {
            // Arrange
            var frame = new Frame("TestFrame");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => frame.SetValue("", "value"));
            Assert.Throws<ArgumentNullException>(() => frame.SetValue(null, "value"));
        }

        [Fact]
        public void AddSlot_Creates_New_Slot_With_Default_Value()
        {
            // Arrange
            var frame = new Frame("TestFrame");

            // Act
            frame.AddSlot("newSlot", "defaultValue");

            // Assert
            Assert.Equal("defaultValue", frame.GetValue("newSlot"));
        }

        [Fact]
        public void AddSlot_Creates_New_Slot_With_Null_Default_Value()
        {
            // Arrange
            var frame = new Frame("TestFrame");

            // Act
            frame.AddSlot("newSlot");

            // Assert
            Assert.Null(frame.GetValue("newSlot"));
        }

        [Fact]
        public void AddSlot_Throws_ArgumentNullException_For_Empty_Slot_Name()
        {
            // Arrange
            var frame = new Frame("TestFrame");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => frame.AddSlot(""));
            Assert.Throws<ArgumentNullException>(() => frame.AddSlot(null));
        }

        [Fact]
        public void GetSlots_Returns_Copy_Not_Reference()
        {
            // Arrange
            var frame = new Frame("TestFrame");
            frame.AddSlot("slot1", "value1");
            frame.AddSlot("slot2", "value2");

            // Act
            var slots1 = frame.GetSlots();
            slots1.Add("slot3", "value3"); // Modify the copy

            var slots2 = frame.GetSlots(); // Get fresh copy

            // Assert
            Assert.Equal(2, frame.GetSlots().Count); // Original unchanged
            Assert.Equal(3, slots1.Count); // First copy modified
            Assert.Equal(2, slots2.Count); // Second copy is fresh
        }

        [Fact]
        public void GetParent_Returns_Null_For_Root_Frame()
        {
            // Arrange
            var frame = new Frame("RootFrame");

            // Act
            var parent = frame.GetParent();

            // Assert
            Assert.Null(parent);
        }

        [Fact]
        public void GetParent_Returns_Correct_Parent()
        {
            // Arrange
            var parentFrame = new Frame("ParentFrame");
            var childFrame = new Frame("ChildFrame", parentFrame);

            // Act
            var parent = childFrame.GetParent();

            // Assert
            Assert.Equal(parentFrame, parent);
        }

        [Fact]
        public void GetName_Returns_Correct_Name()
        {
            // Arrange
            var frame = new Frame("TestFrame");

            // Act
            var name = frame.GetName();

            // Assert
            Assert.Equal("TestFrame", name);
        }

        [Fact]
        public void Complex_Inheritance_Scenario_Works_Correctly()
        {
            // Arrange
            var animal = new Frame("Animal");
            animal.AddSlot("species", "Unknown");
            animal.AddSlot("age", 0);

            var mammal = new Frame("Mammal", animal);
            mammal.AddSlot("furColor", "brown");

            var dog = new Frame("Dog", mammal);
            dog.AddSlot("breed", "Labrador");
            dog.AddSlot("name", "Buddy");

            // Act & Assert
            Assert.Equal("Labrador", dog.GetValue("breed"));
            Assert.Equal("Buddy", dog.GetValue("name"));
            Assert.Equal("brown", dog.GetValue("furColor"));
            Assert.Equal("Unknown", dog.GetValue("species"));
            Assert.Equal(0, dog.GetValue("age"));
        }

        [Fact]
        public void SetValue_Does_Not_Affect_Parent_Frames()
        {
            // Arrange
            var parent = new Frame("Parent");
            parent.AddSlot("sharedSlot", "parentValue");

            var child = new Frame("Child", parent);

            // Act
            child.SetValue("sharedSlot", "childValue");

            // Assert
            Assert.Equal("childValue", child.GetValue("sharedSlot"));
            Assert.Equal("parentValue", parent.GetValue("sharedSlot")); // Parent unchanged
        }

        [Fact]
        public void Frame_With_Different_Value_Types_Works_Correctly()
        {
            // Arrange
            var frame = new Frame("MultiTypeFrame");

            // Act
            frame.AddSlot("stringSlot", "hello");
            frame.AddSlot("intSlot", 42);
            frame.AddSlot("boolSlot", true);
            frame.AddSlot("doubleSlot", 3.14);
            frame.AddSlot("objectSlot", new List<int> { 1, 2, 3 });
            frame.AddSlot("nullSlot");

            // Assert
            Assert.Equal("hello", frame.GetValue("stringSlot"));
            Assert.Equal(42, frame.GetValue("intSlot"));
            Assert.True((bool)frame.GetValue("boolSlot"));
            Assert.Equal(3.14, frame.GetValue("doubleSlot"));
            Assert.IsType<List<int>>(frame.GetValue("objectSlot"));
            Assert.Null(frame.GetValue("nullSlot"));
        }

        [Fact]
        public void Shadowing_Parent_Slot_Works_Correctly()
        {
            // Arrange
            var parent = new Frame("Parent");
            parent.AddSlot("slot", "parentValue");

            var child = new Frame("Child", parent);

            // Act
            child.AddSlot("slot", "childValue"); // Shadows parent slot

            // Assert
            Assert.Equal("childValue", child.GetValue("slot"));
            Assert.Equal("parentValue", parent.GetValue("slot"));
        }

        [Fact]
        public void Multiple_Children_Share_Parent_Slots_Independently()
        {
            // Arrange
            var parent = new Frame("Parent");
            parent.AddSlot("sharedSlot", "parentValue");

            var child1 = new Frame("Child1", parent);
            var child2 = new Frame("Child2", parent);

            // Act
            child1.SetValue("sharedSlot", "child1Value");
            child2.SetValue("sharedSlot", "child2Value");

            // Assert
            Assert.Equal("child1Value", child1.GetValue("sharedSlot"));
            Assert.Equal("child2Value", child2.GetValue("sharedSlot"));
            Assert.Equal("parentValue", parent.GetValue("sharedSlot"));
        }

        [Fact]
        public void Deep_Inheritance_Chain_Performs_Correctly()
        {
            // Arrange
            Frame current = new Frame("Level0");
            current.AddSlot("slot0", "value0");

            for (int i = 1; i <= 10; i++)
            {
                current = new Frame($"Level{i}", current);
                current.AddSlot($"slot{i}", $"value{i}");
            }

            // Act & Assert - Should find slots from all levels
            for (int i = 0; i <= 10; i++)
            {
                Assert.Equal($"value{i}", current.GetValue($"slot{i}"));
            }

            // Should not find non-existent slot
            Assert.Throws<KeyNotFoundException>(() => current.GetValue("nonExistentSlot"));
        }

        [Fact]
        public void Frame_Can_Be_Used_As_Value_In_Another_Frame()
        {
            // Arrange
            var addressFrame = new Frame("Address");
            addressFrame.AddSlot("street", "123 Main St");
            addressFrame.AddSlot("city", "Springfield");

            var personFrame = new Frame("Person");
            personFrame.AddSlot("name", "John Doe");
            personFrame.AddSlot("address", addressFrame);

            // Act
            var name = personFrame.GetValue("name");
            var address = personFrame.GetValue("address") as Frame;
            var street = address.GetValue("street");
            var city = address.GetValue("city");

            // Assert
            Assert.Equal("John Doe", name);
            Assert.NotNull(address);
            Assert.Equal("Address", address.GetName());
            Assert.Equal("123 Main St", street);
            Assert.Equal("Springfield", city);
        }
    }
}
