import { Component, Input, OnInit } from '@angular/core';
import { Fruit } from '../../models/fruit';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss'],
})
export class ModalComponent implements OnInit {
  @Input() Fruit: Fruit;

  constructor() {}

  ngOnInit() {
    this.getAlbuns();
  }

  getAlbuns(): any {}

  getSrc(imagem: any) {
    // return imagem.url;
  }
}
