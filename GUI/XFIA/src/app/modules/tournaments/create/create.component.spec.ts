import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from "@angular/core/testing";
import { CreateComponent } from "./create.component";
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
describe("CreateComponent", () => {
  let component: CreateComponent;
  let fixture: ComponentFixture<CreateComponent>;


  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateComponent],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    
      imports: [HttpClientTestingModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

   
  });

  describe('method1', () => {
    it('should ...', () => {
      expect(component).toBeTruthy();
    });
  });
})