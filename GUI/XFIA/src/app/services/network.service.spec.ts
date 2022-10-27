import { TestBed } from '@angular/core/testing';
import { NetworkService } from "./network.service";
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
describe("NetworkService", () => {
  let service: NetworkService;
  let controller:HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports:[HttpClientTestingModule]
    });
    service = TestBed.inject(NetworkService);
    controller=TestBed.inject(HttpTestingController)
  });

  describe('creating', () => {
    it('create', () => {
      expect(service).toBeTruthy();
    });
  });

  describe('request testing', ()=> {
    it('request api star wars api', () => {
      
      service.setIp("https://swapi.dev/api/");
      console.log(service)
      service.get_request('people/1',{}).subscribe(
        (people:any)=>{
          expect(people.name).toEqual("Luke Skywalker")
          
        },
        ()=>{
          expect(true).toBe(false)
        }
      )
      const request=controller.expectOne('https://swapi.dev/api/people/1')
      request.flush(
        {
          "name": "Luke Skywalker",
          "height": "172",
          "mass": "77",
          "hair_color": "blond",
          "skin_color": "fair",
          "eye_color": "blue",
          "birth_year": "19BBY",
          "gender": "male",
          "homeworld": "https://swapi.dev/api/planets/1/",
          "films": [
            "https://swapi.dev/api/films/1/",
            "https://swapi.dev/api/films/2/",
            "https://swapi.dev/api/films/3/",
            "https://swapi.dev/api/films/6/"
          ],
          "species": [],
          "vehicles": [
            "https://swapi.dev/api/vehicles/14/",
            "https://swapi.dev/api/vehicles/30/"
          ],
          "starships": [
            "https://swapi.dev/api/starships/12/",
            "https://swapi.dev/api/starships/22/"
          ],
          "created": "2014-12-09T13:50:51.644000Z",
          "edited": "2014-12-20T21:17:56.891000Z",
          "url": "https://swapi.dev/api/people/1/"
        }
      )

     
    });
  });
});
