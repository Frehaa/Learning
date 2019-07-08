object Main {
  def main(args: Array[String]): Unit = {
    println("Hello, world!")


    val x : Option[Int] = None

    val y = x match {
      case Some(v) => v
      case None => 0
    }

    val q = for {
      v1 <- x
      v2 <- x
    } yield v1 + v2

    println(q)
  }
}
