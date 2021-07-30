import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Animal } from '../model/animal';

@Component({
  selector: 'app-animal-profile',
  templateUrl: './animal-profile.component.html',
  styleUrls: ['./animal-profile.component.css']
})
export class AnimalProfileComponent implements OnInit {

  @ViewChild('animalForm', { static: false }) animalForm: NgForm;
  public animal: Animal = Animal.empty();
  public originalAnimal: Animal = Animal.empty();
  public successMessage: string;
  public showSuccessful = true;

  constructor() { }

  ngOnInit(): void {
  }

  saveAnimal() {

  }

}
