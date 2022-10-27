
import { carInterface,pilotInterface } from "../interface/interfaces";
import{budgedCalc} from './budgetCalc'
describe("calc price suite", function() {
    it("cost calc team", function() {
        var pilotMap= new Map<number,pilotInterface>()
        var sum=600;
        pilotMap.set(1,{
            Id:1,
            Firstname:'',
            Lastname:'',
            Photo:'',
            Price:100,
            CountryNameNavigation:null,
            RealTeamsNameNavigation:null
        
        })
        pilotMap.set(2,{
            Id:1,
            Firstname:'',
            Lastname:'',
            Photo:'',
            Price:100,
            CountryNameNavigation:null,
            RealTeamsNameNavigation:null
        
        })
        pilotMap.set(3,{
            Id:1,
            Firstname:'',
            Lastname:'',
            Photo:'',
            Price:100,
            CountryNameNavigation:null,
            RealTeamsNameNavigation:null
        
        })
        pilotMap.set(4,{
            Id:1,
            Firstname:'',
            Lastname:'',
            Photo:'',
            Price:100,
            CountryNameNavigation:null,
            RealTeamsNameNavigation:null
        
        })
        pilotMap.set(5,{
            Id:1,
            Firstname:'',
            Lastname:'',
            Photo:'',
            Price:100,
            CountryNameNavigation:null,
            RealTeamsNameNavigation:null
        
        })
        var carInterface:carInterface={
            Name:'',
            Photo:'',
            Price:100
        }
        var price=budgedCalc.calculateTeamCost(pilotMap,carInterface)
        expect(price).toEqual(sum);


     
      
    });
  });