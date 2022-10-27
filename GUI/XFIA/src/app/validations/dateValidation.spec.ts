import{dateValidations} from './dateValidations'

describe("dates suite", function() {
    it("past date", function() {
        var date='2000-04-30';
        var pastCondition=dateValidations.inThePast(date);
        expect(pastCondition).toEqual(true);


        date='2090-04-30';
        pastCondition=dateValidations.inThePast(date);
        expect(pastCondition).toEqual(false);

    });
  });

  describe('dates suite',function(){
    it('Logic Sequence Time',function(){
        var initialDate='2000-04-30';
        var finalDate='2000-05-30'
        var Condition=dateValidations.continousDate(initialDate,finalDate);
        expect(Condition).toEqual(true);

        var initialDate='2000-05-30';
        var finalDate='2000-05-30'
        var Condition=dateValidations.continousDate(initialDate,finalDate);
        expect(Condition).toEqual(false);

    })
  })