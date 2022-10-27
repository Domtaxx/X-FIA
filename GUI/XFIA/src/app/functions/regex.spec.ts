import { checkRegex } from "./regex";

describe("regex suite", function() {
    it("regex prove", function() {
      var regex:RegExp=/^(([a-zA-Z]*[0-9]*)*[a-zA-Z]+([a-zA-Z]*[0-9]*)*[0-9]+([a-zA-Z]*[0-9]*)*)*(([a-zA-Z]*[0-9]*)*[0-9]+([a-zA-Z]*[0-9]*)*[a-zA-Z]+([a-zA-Z]*[0-9]*)*)*$/;
      var expresion:string='luis1234';
      var result:boolean=checkRegex(regex,expresion);
      expect(result).toEqual(true);


      regex=/^(([a-zA-Z]*[0-9]*)*[a-zA-Z]+([a-zA-Z]*[0-9]*)*[0-9]+([a-zA-Z]*[0-9]*)*)*(([a-zA-Z]*[0-9]*)*[0-9]+([a-zA-Z]*[0-9]*)*[a-zA-Z]+([a-zA-Z]*[0-9]*)*)*$/;
      expresion='1212122121221212';
      result=checkRegex(regex,expresion);
      expect(result).toEqual(false);


      regex=/^[1-9]*$/;
      expresion='1212122121221212';
      result=checkRegex(regex,expresion);
      expect(result).toEqual(true);


      regex=/^[1-9]*$/;
      expresion='1212122d121221212';
      result=checkRegex(regex,expresion);
      expect(result).toEqual(false);
      
    });
  });