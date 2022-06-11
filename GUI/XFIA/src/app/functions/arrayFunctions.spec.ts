import{orderNumberArray} from './arrayFunctions'
import{repitedElement}from './arrayFunctions'


describe("array suites", function() {
    it("find repeted element", function() {
      var numberArray:number[]=[1,2,3,4,5];
      var repitedState:boolean=repitedElement(numberArray);
      expect(repitedState).toBe(false);


      numberArray=[1,2,3,4,5,1,6];
      repitedState=repitedElement(numberArray);
      expect(repitedState).toBe(true);


      numberArray=[10,22,44,6,2,4,5];
      repitedState=repitedElement(numberArray);
      expect(repitedState).toBe(false);
    });
  });

  describe("array suites", function() {
    it("orderedNumberArray", function() {
      var numberArray:number[]=[5,4,3,2,1];
      var orderedArrayExpectedResult:number[]=[1,2,3,4,5];
      var orderedArrayResult:number[]=orderNumberArray(numberArray);
      expect(orderedArrayExpectedResult).toEqual(orderedArrayResult);

      var numberArray:number[]=[3,4,1,2,5];
      var orderedArrayExpectedResult:number[]=[1,2,3,4,5];
      var orderedArrayResult:number[]=orderNumberArray(numberArray);
      expect(orderedArrayExpectedResult).toEqual(orderedArrayResult);

      var numberArray:number[]=[5,1,4,2,3];
      var orderedArrayExpectedResult:number[]=[1,2,3,4,5];
      var orderedArrayResult:number[]=orderNumberArray(numberArray);
      expect(orderedArrayExpectedResult).toEqual(orderedArrayResult);
    

    });
  });