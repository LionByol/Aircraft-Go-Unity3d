using UnityEngine;
using System.Collections;

public class RescueMission : MonoBehaviour {

	// Use this for initialization
	void Start () {
		person.sprite2D = persons [R.selectedLevel];
		goalLab.text = "Collect " + R.goal_coin [R.selectedLevel];
		commentLab.text = comments [R.selectedLevel];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBack()
	{
		Splash.instance.BackScene ();
	}

	public void OnPlay()
	{
		R.gameMode = 1;		//rescue mission mode
		Splash.instance.LoadScene ("game");
	}

	string[] comments = new string[]{ 
		"Officer Randy McCarlson was investigating an alley where a woman cried in distress when he was shoved hard into the ground from behind. When he awoke, he found himself tied inside a coffin he could only assume was buried beneath the ground. He was a favorite among the neighborhood and his three-year-old daughter, Caitlin, frantically continued searching for him.",
		"Hayden Simpson was a new pilot tackling his first flight. He had no family and his parents did when he was young, but his grandmother was extremely proud of him. While preparing in the bathroom, a clocked figure approached from behind and smashed his head into the window. Next, he awoke lying flat on a dock in the middle of the lake. He was a quadriplegic.",
		"Ellie Hobbs was a nosy Manager who always got into everyone’s personal business. He was demanding and rarely satisfied, but always stood behind his workers. While inspecting the worksite late night, Ellie is jumped from behind and knock out. He awoke to find himself in a scuba suit and weight down in murky water. He was forced to watch his oxygen level deplete as he wondered whether or not he’d ever see his newly wed wife again.",
		"Wendal Carlson was a family doctor with a broken family of his own. While his patients loved him, his ex-wife despised the fact that he worked so hard. However, after receiving an unexpected message from her to pick up their two kids for the weekend, he arrived to find them all dead in their beds. A sudden darkness washed over him as he found himself in a filthy room filled with sick people with no chance of survival.",
		"Connor Icken always loved numbers and found mathematical sequences in everything he did. He was never in a relationship, but he always liked a female coworker. On the day he decided to ask her out, Connor was attacked and stabbed in the stomach while passing through an alleyway. Now, he found himself locked in a room and calculating his survival percentage as water rushed inside.",
		"Jordan Williams dreamt of becoming a famous photographer. He brought smiles to everyone’s faces when he posted photos under an alias from exotic places. While hiking in a dense forest, he was chased by a horrifying snakelike creature, which devoured him. He woke up in a dark room with nothing to photograph. Since he was an only child and his parents recently died in a car crash, no one is currently missing him.",
		"Joseph Zakara was an exceptional mailman, going above and beyond his job description. While filling a mailbox, he witnessed a man murdered by a masked man behind the fence. Before he could react, he was knocked unconscious and awoke in a cellar filled with his worst fear: spiders. His wife and son aren’t looking for him, because the kidnapper faked a divorce letter from him.",
		"Serving the A-list crowd, Kyle Johnston had heard his fair share of gossip. After overhearing a man confess to killing his wife, Kyle calls his girlfriend to record the conversation. While on the phone, his girlfriend is forced to listen as Kyle’s dragged into the alley and beaten repeatedly. His current state is unknown.",
		"Although he hated his work, Jacob McDonald was a third generation plumber. He was married to a drunk with three kids who hated him. While out on a call, a chloroform rag was shoved over his mouth and he awoke in an abandoned building that was flooding quickly. With all the doors locked, it almost seemed futile as Jacob desperately tried to repair all the plumbing issues.",
		"A bit of a grease ball, Jonathon Frair took his work quite seriously. He worked long hours, barely visited his parents and neve refused a case, regardless if it was ethical or not. Working late at the office, he heard heavy footsteps outside his office. Investigating the noise, Jonathon is suddenly cloaked in darkness only to find himself locked in an abandoned courtroom covered in blood, the source of which is unknown.",
		"The Mackay farm had provided fresh food for the community for over 80 years. Stan Mackay III had recently begun teaching his only son the trade. Noticing a strange shadow in his son’s room one night, Stan went to investigate and was knocked unconscious. He awoke in an abandoned complex with images depicting his son’s possible murder.",
		"Johnny Morris was a lady’s man when out on a call. He took more pride in pleasing a woman than being an electrician. After a night out with the boys, Johnny followed a woman out, only for her to mysteriously disappear around the corner. Instead, a black figure ran appeared and cloaked him in darkness. He reemerged chained to a tree in the middle of nowhere. He’s divorced but had a young daughter who cherished daddy time.",
		"Steve Morrison loved his work and came home to a loving girlfriend and son. He was excited for the day he would begin training his son to be an electrician. While out on a late night call, Steve is jumped from behind and strapped to an electrical barbed wire. With an intense shock surging through his body every ten minutes, he struggles with the knowledge that he’ll soon be dead.",
		"Edward Eisel was a renowned violinist who played across the globe. His loving parents followed him to several performances, continuing to have a close bond with him. Setting up for a show one morning, Edward was knocked unconscious with his violin. He woke up tied to a chair with a cloaked figure breaking his fingers one by one. His parents spearheaded a frantic search for their beloved son.",
		"After finishing culinary school, Chef Thomas Ellis opened up his first family-friendly restaurant. Without a family of his own, he treated every customer like they were. Cleaning up one night, Thomas heard a strange noise in the portable refrigerator and went to inspect it. Opening the lid, Thomas was suddenly thrown inside with the door locked behind him. The dark force wheeled him away, trapping him inside with no way out.",
		"Bill Kershaw owned several houses and cars due to illegally selling insider information online. On his way to another shady deal, Bill was suddenly cloaked in darkness as he was beaten nearly to death. He awoke inside an abandoned building riddled with piles of his money burning around him. Video cameras record his assets burning across the globe. No one mourned his disappearance, except for his Golden Retriever who had been his faithful companion for ten years.",
		"After witnessing his mother die in a house fire, Tim O’Keefe decided to become a fireman, saving many lives in the process. As his team attempted to put out an apartment building fire, Tim thought he saw a young mother and baby in an apartment. Realizing it was his imagination, he was suddenly shoved into the fire pit below. Before he was burned, Tim found himself in a room with a never-ending fire surrounding him. His pregnant wife was falsely told he may have perished in the fire.",
		"Erwin Hicklestone was everyone’s favorite contractor, being highly requested by everyone he serviced. Heading home for date night with his wife of fifteen years, Erwin’s car is sudden struck from the side. He awoke to find himself handcuffed to a bed in a room much like his own. A video recording revealing his wife’s affair played on repeat as he begged it to stop.",
		"Leo Hill was an up and coming artist when he mysteriously disappeared from his studio one night. All that was left behind were his two severed hands and a new painting depicting his mutilated corpse. In actuality, Leo was kidnapped and forced to paint his body in horrendous death scenes, without the use of his hands.",
		"Eddie Bynes was a eminent singer with many beloved shows under her belt. As he drove to his newest gig with his wife and three children, a car swerved and crashed head on, killing everyone but Eddie. Waking up, he found himself in a locked and abandoned building. The walls were covered with photos of the crash site with someone commentating over the speakerphone.",
		"Phil Murphy was a college soccer player with a bright future ahead of him. While practicing late one night, Phil noticed at cloaked figure stalking him off field. He called his parents who instructed him to head home. As he bolted away, darkness fell over him and lifted to reveal that he was running in a creepy forest. With no evident end to the forest, Phil was forced to continue running as the cloaked figure pursued him."
	};

	public Sprite[] persons;
	public UILabel goalLab;
	public UILabel commentLab;
	public UI2DSprite person;
}
