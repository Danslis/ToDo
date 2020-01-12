export class Card {
  constructor(data: any) {
    this.id = data.id;
    this.date = data.date;
    this.name = data.name;
    this.timeOfCompletion = data.timeOfCompletion;
    this.priority = data.priority;
    this.position = data.position;
  }

  id: number = 0;
  name: string;
  date: Date = new Date();
  timeOfCompletion: number;
  priority: number;
  position: number;
}
