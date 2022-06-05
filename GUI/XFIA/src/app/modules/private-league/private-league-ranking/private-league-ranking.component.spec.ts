import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrivateLeagueRankingComponent } from './private-league-ranking.component';

describe('PrivateLeagueRankingComponent', () => {
  let component: PrivateLeagueRankingComponent;
  let fixture: ComponentFixture<PrivateLeagueRankingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrivateLeagueRankingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrivateLeagueRankingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
